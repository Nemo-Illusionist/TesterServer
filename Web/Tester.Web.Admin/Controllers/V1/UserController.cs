using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Db.Model.Client;
using Tester.Dto.Users;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class UserController : BaseCrudController<IUserService, User, Guid, UserDto, UserDto, UserRequest>
    {
        public UserController([NotNull] IUserService crudService, [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public Task<IActionResult> Create(UserRequest request)
        {
            return Add(request);
        }
    }
}