using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Core.Exception;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Auth.Contracts;
using Tester.Core.Common;
using Tester.Db.Model.Client;
using Tester.Dto.Users;
using Tester.Infrastructure.Contracts;

namespace Tester.Web.Admin.Services
{
    public class UserService : BaseRoService<User, Guid, UserDto, UserDto>, IUserService
    {
        private readonly IRwDataProvider _rwDataProvider;
        private readonly IPasswordProvider _passwordProvider;

        public UserService(IAsyncHelpers asyncHelpers,
            IOrderHelper orderHelper,
            IMapper mapper,
            [NotNull] IRwDataProvider rwDataProvider,
            [NotNull] IPasswordProvider passwordProvider)
            : base(rwDataProvider, asyncHelpers, orderHelper, mapper)
        {
            _rwDataProvider = rwDataProvider ?? throw new ArgumentNullException(nameof(rwDataProvider));
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
        }

        public async Task<Guid> Post(UserRequest request)
        {
            var user = Mapper.Map<User>(request);
            var userData = Mapper.Map<UserData>(request);
            var passwordHash = _passwordProvider.CreatePasswordHash(request.Password);
            user.Password = passwordHash.Hash;
            user.Salt = passwordHash.Salt;
            user.SecurityTimestamp = Guid.NewGuid();
            userData.Gender = Gender.Undefined;

            await using var transaction = _rwDataProvider.Transaction();
            await _rwDataProvider.InsertAsync(user);
            userData.UserId = user.Id;
            await _rwDataProvider.InsertAsync(userData);
            await transaction.CommitAsync();
            return user.Id;
        }

        public async Task<Guid> Put(Guid id, UserRequest request)
        {
            var user = await _rwDataProvider.GetQueryable<User>().Where(x=>x.Id == id)
                .Include(x=>x.UserData)
                .SingleOrDefaultAsync();
            if(user == null) throw new ItemNotFoundException();
            
            user = Mapper.Map(request, user);
            user.UserData = Mapper.Map(request, user.UserData);

            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}