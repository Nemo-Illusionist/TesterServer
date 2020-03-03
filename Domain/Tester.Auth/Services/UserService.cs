using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using REST.Core.Exception;
using REST.DataCore.Contract;
using REST.DataCore.Contract.Provider;
using Tester.Db.Model.Client;
using Tester.Dto.Users;

namespace Auth.Services
{
    public class UserService: IUserService
    {
        private readonly PasswordHash hashPassword;
        private readonly IRoDataProvider _dataProvider;
        private IConfigurationProvider Mapper { get; }
        private IAsyncHelpers AsyncHelpers { get; }
        public UserService([NotNull] IAsyncHelpers asyncHelpers)
        {
            hashPassword = new PasswordHash();
            AsyncHelpers = asyncHelpers ?? throw new ArgumentNullException(nameof(asyncHelpers));
        }

        public async Task<User> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;
            
            var queryable = _dataProvider.GetQueryable<User>().Where(x => x.Login.Equals(login));
            var user = await AsyncHelpers.SingleOrDefaultAsync(queryable)
                .ConfigureAwait(false);

            if (user == null) throw new ItemNotFoundException();

            // check if password is correct
            if (!hashPassword.VerifyPasswordHash(password, user.Password, user.Salt))
                return null;

            // authentication successful
            return user;
        }
    }
}