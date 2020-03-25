using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tester.Auth.Contracts;

namespace Tester.Auth.Services
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly AuthOptions _authOptions;

        public JwtTokenProvider([NotNull] IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentNullException(nameof(authOptions));
        }

        public string Generate([NotNull] ClaimsIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[0]
                    .Concat(identity.Claims)),
                Expires = DateTime.UtcNow.AddDays(_authOptions.Duration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}