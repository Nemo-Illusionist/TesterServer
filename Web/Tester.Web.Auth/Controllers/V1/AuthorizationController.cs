using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Tester.Auth.Contracts;
using Tester.Dto.User;
using Tester.Web.Core.Controllers;

namespace Tester.Web.Auth.Controllers.V1
{
    public class AuthorizationController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthorizationController([NotNull] IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authorization([FromBody] LoginDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new {message = "Login or password is incorrect"});

            try
            {
                var token = await _authService.Authenticate(model.Login, model.Password)
                    .ConfigureAwait(false);
                SetTokenToCookie(token);
                return Ok(new TokenDto {Token = token});
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        private void SetTokenToCookie(string token)
        {
            HttpContext.Response.Cookies.Append("access_token", token);
        }
    }
}