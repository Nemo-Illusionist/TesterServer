using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Tester.Auth.Contracts;
using Tester.Dto;
using Tester.Dto.User;
using Tester.Web.Admin.Controllers.Base;

namespace Tester.Web.Admin.Controllers.V1
{
    public class AuthorizationController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthorizationController([NotNull] IAuthService authService,
            [NotNull] IValidatorFactory validatorFactory)
            : base(validatorFactory)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseDto<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authorization([FromBody] AuthenticateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new {message = "Login or password is incorrect"});

            try
            {
                var token = await _authService.Authenticate(model.Login, model.Password).ConfigureAwait(false);
                SetTokenToCookie(token);
                return Ok(token);
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