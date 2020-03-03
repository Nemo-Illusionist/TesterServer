using System;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Helpers;
using Auth.Services;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using REST.Infrastructure.Contract;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Dto.Users;
using Tester.Infrastructure.Сontracts;
using Tester.Web.Admin.Controllers.Base;

namespace Tester.Web.Admin.Controllers.V1
{
    public class AuthorizationController : BaseRoV1Controller<IAuthService, User, Guid, BaseDto<Guid>, BaseDto<Guid>>
    {
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthorizationController([NotNull] IAuthService crudService,
            [NotNull] IFilterHelper filterHelper, IMapper mapper, IOptions<AppSettings> appSettings)
            : base(crudService, filterHelper)
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
            
        }
    }
}