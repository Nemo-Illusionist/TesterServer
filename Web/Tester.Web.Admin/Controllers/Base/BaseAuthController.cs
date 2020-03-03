using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Helpers;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using REST.DataCore.Contract;
using System.IdentityModel.Tokens.Jwt;

namespace Tester.Web.Admin.Controllers.Base
{
    public class BaseAuthController
    {
        private readonly UserService _auth;
        private IAsyncHelpers AsyncHelpers { get; }
        public BaseAuthController()
        {
            _auth = new UserService(AsyncHelpers);
        }
        protected async Task<IActionResult> Authorization(string Login, string Password)
        {
            var user = await _auth.Authenticate(Login, Password).ConfigureAwait(false);;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token); 
        }
    }
}