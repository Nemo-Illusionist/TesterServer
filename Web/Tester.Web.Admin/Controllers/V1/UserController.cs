using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tester.Db.Model.Client;
using Tester.Dto.Users;
using Tester.Web.Admin.Controllers.Base;
using Tester.Infrastructure.Contracts;
using System.Diagnostics.CodeAnalysis;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class UserController : BaseCrudController<IUserService, User, Guid, UserDto, UserDto, UserRequest>
    {
        public UserController([NotNull] IUserService crudService, [NotNull] IFilterHelper filterHelper) : base(crudService, filterHelper)
        { }


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
        
    }
}

