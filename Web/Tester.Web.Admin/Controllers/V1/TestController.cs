using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tester.Db.Model.Client;

using Tester.Db.Model.App;
using Tester.Dto.Tests;
using Tester.Web.Admin.Controllers.Base;
using Tester.Infrastructure.Contracts;
using System.Diagnostics.CodeAnalysis;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Web.Admin.Models;



namespace Tester.Web.Admin.Controllers.V1
{

    public class TestController : BaseCrudController<ITestService, Test, Guid, TestDto, TestDto, TestRequest>
    {
        public TestController([NotNull] ITestService crudService, [NotNull] IFilterHelper filterHelper) : base(crudService, filterHelper)
        { }

        

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TestDto>), 200)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TestDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public Task<IActionResult> Get(Guid id)
            {
                return GetById(id);
            }

        [HttpPost]
        [ProducesResponseType(typeof(TestDto), 200)]
        public Task<IActionResult> Create(TestRequest request)
        {
            return Add(request);
        }
    }
}