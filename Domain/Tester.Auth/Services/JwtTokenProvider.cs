using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tester.Auth.Contracts;
using Tester.Auth.Models;

namespace Tester.Auth.Services
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly TokenAuthOptions _tokenAuthOptions;
        private readonly AuthOptions _authOptions;

        public JwtTokenProvider([NotNull] IOptions<AuthOptions> authOptions,
            IOptions<TokenAuthOptions> tokenAuthOptions)
        {
            _tokenAuthOptions = tokenAuthOptions?.Value ?? throw new ArgumentNullException(nameof(tokenAuthOptions));
            _authOptions = authOptions?.Value ?? throw new ArgumentNullException(nameof(authOptions));
        }

        public async Task<string> Generate([NotNull] ClaimsIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));

            var now = DateTime.UtcNow;

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,
                        await _tokenAuthOptions.NonceGenerator().ConfigureAwait(false)),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(DateTime.UtcNow).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)
                }.Concat(identity.Claims)
                .ToArray();

            var jwt = new JwtSecurityToken(
                _tokenAuthOptions.Issuer,
                _tokenAuthOptions.Audience,
                claims,
                now,
                now.Add(_tokenAuthOptions.Expiration),
                _tokenAuthOptions.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}