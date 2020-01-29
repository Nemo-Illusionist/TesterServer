using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.Infrastructure.Contract;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Infrastructure.Сontracts;
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
        public override Task<IActionResult> GetByFilter([FromQuery] FilterRequest filter)
        {
            return base.GetByFilter(filter);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> GetById(Guid id)
        {
            return base.GetById(id);
        }
    }
}