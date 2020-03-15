using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Tester.Auth.Contracts;
using Tester.Dto;
using Tester.Dto.Users;
using Tester.Web.Admin.Controllers.Base;

namespace Tester.Web.Admin.Controllers.V1
{
    public class AuthorizationController : BaseV1Controller
    {
        private readonly IAuthService _authService;

        public AuthorizationController([NotNull] IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Authorization([FromBody] [NotNull] AuthenticateModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new {message = "Login or password is incorrect"});
            
            var token = await _authService.Authenticate(model.Login, model.Password)
                .ConfigureAwait(false);

            return Ok(token);
        }
    }
}