using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Dto.Users;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class UserController :  BaseCrudController<IUserService, User, Guid, UserDto, UserDto, UserRequest>
    {
        public UserController([NotNull] IUserService crudService, [NotNull] IFilterHelper filterHelper) : base(crudService, filterHelper)
        {
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<UserDto>), 200)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 200)]
        public Task<IActionResult> Create(UserRequest request)
        {
            return Add(request);
        }
    }
}