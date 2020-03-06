using System;
using System.Threading.Tasks;
using Auth.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tester.Dto;
using Tester.Dto.Users;
using Options = Auth.Helpers.Options;

namespace Tester.Web.Admin.Controllers.V1
{
    public class AuthorizationController
    {
        private IMapper _mapper;
        private readonly Options _appSettings;
        private readonly UserService _auth;
        public AuthorizationController(IMapper mapper, IOptions<Options> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Authorization([FromBody] AuthenticateModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { message = "Username or password is incorrect" });
            var token = await _auth.Authenticate(model.Login, model.Password)
                .ConfigureAwait(false);
        }
    }
}