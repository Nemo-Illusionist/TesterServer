using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Auth.Contracts;
using Tester.Auth.Models;
using Tester.Db.Model.Client;

namespace Tester.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRoDataProvider _dataProvider;
        private readonly IAsyncHelpers _asyncHelpers;
        private readonly ITokenProvider _tokenProvider;
        private readonly IPasswordProvider _passwordProvider;

        public AuthService([NotNull] IRoDataProvider dataProvider,
            [NotNull] IAsyncHelpers asyncHelpers,
            [NotNull] ITokenProvider tokenProvider,
            [NotNull] IPasswordProvider passwordProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _asyncHelpers = asyncHelpers ?? throw new ArgumentNullException(nameof(asyncHelpers));
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _passwordProvider = passwordProvider ?? throw new ArgumentNullException(nameof(passwordProvider));
        }

        public async Task<string> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var queryable = _dataProvider.GetQueryable<User>().Where(x => x.Login == login)
                .Include(x => x.UserData)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role);
            var user = await _asyncHelpers.SingleOrDefaultAsync(queryable)
                .ConfigureAwait(false);

            if (user == null) throw new ItemNotFoundException();

            var passwordHash = new PasswordHash(user.Password, user.Salt);
            if (!_passwordProvider.VerifyPasswordHash(password, passwordHash))
            {
                throw new ItemNotFoundException();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserData.Name),
                new Claim("userId", user.Id.ToString()),
                new Claim("login", user.Login)
            };

            var rolesClaims = user.UserRoles.Select(r =>
                new Claim(ClaimsIdentity.DefaultRoleClaimType, r.Role.Name));
            claims.AddRange(rolesClaims);

            var id = new ClaimsIdentity(claims, "Cookies");
            return await _tokenProvider.Generate(id).ConfigureAwait(false);
        }
    }
}