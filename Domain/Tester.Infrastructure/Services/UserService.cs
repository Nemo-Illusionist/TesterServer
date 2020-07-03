using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Auth.Contracts;
using Tester.Core.Common;
using Tester.Db.Model.Client;
using Tester.Dto.User;
using Tester.Infrastructure.Contracts;

namespace Tester.Infrastructure.Services
{
    public class 
        UserService : BaseCrudService<User, Guid, UserDto, UserDto, UserRequest>, IUserService
    {
        private readonly IPasswordProvider _passwordProvider;

        public UserService(IAsyncHelpers asyncHelpers,
            IOrderHelper orderHelper,
            IMapper mapper,
            IDataProvider dataProvider,
            [NotNull] IPasswordProvider passwordProvider)
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
        }

        public override async Task<Guid> Post([NotNull] UserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = Mapper.Map<User>(request);
            var userData = Mapper.Map<UserData>(request);
            var passwordHash = _passwordProvider.CreatePasswordHash(request.Password);
            user.Password = passwordHash.Hash;
            user.Salt = passwordHash.Salt;
            user.SecurityTimestamp = Guid.NewGuid();
            userData.Gender = Gender.Undefined;
            var userRole = new UserRole {RoleId = request.RoleId};

            await using var transaction = DataProvider.Transaction();
            await DataProvider.InsertAsync(user).ConfigureAwait(false);

            userData.UserId = user.Id;
            await DataProvider.InsertAsync(userData).ConfigureAwait(false);

            userRole.UserId = user.Id;
            await DataProvider.InsertAsync(userRole).ConfigureAwait(false);

            await transaction.CommitAsync().ConfigureAwait(false);
            return user.Id;
        }

        public override async Task<Guid> Put(Guid id, [NotNull] UserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await using var transaction = DataProvider.Transaction();
            var user = await RoDataProvider.GetQueryable<User>().Where(x => x.Id == id)
                .Include(x => x.UserData)
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            if (user == null) throw new ItemNotFoundException();

            user = Mapper.Map(request, user);
            user.UserData = Mapper.Map(request, user.UserData);
            await DataProvider.UpdateAsync(user).ConfigureAwait(false);
            await UpdateRole(request, user).ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);
            return user.Id;
        }

        private async Task UpdateRole(UserRequest request, User user)
        {
            if (user.UserRoles.Any(x => x.RoleId == request.RoleId && x.DeletedUtc.HasValue))
            {
                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.RoleId == request.RoleId)
                    {
                        userRole.DeletedUtc = null;
                    }
                    else if (userRole.DeletedUtc == null)
                    {
                        userRole.DeletedUtc = DateTime.UtcNow;
                    }
                }

                await DataProvider.BatchUpdateAsync(user.UserRoles).ConfigureAwait(false);
            }
            else if (user.UserRoles.All(x => x.RoleId != request.RoleId))
            {
                foreach (var userRole in user.UserRoles)
                {
                    userRole.DeletedUtc ??= DateTime.UtcNow;
                }

                await DataProvider.BatchUpdateAsync(user.UserRoles).ConfigureAwait(false);
                var role = new UserRole {RoleId = request.RoleId, UserId = user.Id};
                await DataProvider.InsertAsync(role).ConfigureAwait(false);
            }
        }
    }
}