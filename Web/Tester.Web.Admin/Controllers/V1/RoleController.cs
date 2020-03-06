using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.Infrastructure.Contract;
using REST.Infrastructure.Dto;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class RoleController : BaseRoV1Controller<IRoleRoService, Role, Guid, BaseDto<Guid>, BaseDto<Guid>>
    {
        public RoleController([NotNull] IRoleRoService crudService,
            [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<BaseDto<Guid>>), 200)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }
    }
}