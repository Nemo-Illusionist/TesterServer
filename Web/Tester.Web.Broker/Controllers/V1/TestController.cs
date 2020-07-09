using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Db.Model.App;
using Tester.Dto;
using Tester.Dto.Test;
using Tester.Infrastructure.Contracts;
using Tester.Web.Broker.Controllers.Base;

namespace Tester.Web.Broker.Controllers.V1
{
    public class TestController : BaseBrokerRoController<ITestService, Test, Guid, TestDto, TestFullDto>
    {
        public TestController([NotNull] ITestService crudService,
            [NotNull] IFilterHelper filterHelper,
            [NotNull] IValidatorFactory validatorFactory)
            : base(crudService, filterHelper, validatorFactory)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TestDto>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get([FromBody] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }
    }
}