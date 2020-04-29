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
using Tester.Dto.Topics;

namespace Tester.Web.Admin.Controllers.V1
{

    public class TopicController : BaseCrudController<ITopicService, Topic, Guid, TopicDto, TopicDto, TopicRequest>
    {
        public TopicController([NotNull] ITopicService crudService, [NotNull] IFilterHelper filterHelper) : base(crudService, filterHelper)
        { }

        

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TopicDto>), 200)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TopicDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public Task<IActionResult> Get(Guid id)
            {
                return GetById(id);
            }

        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public Task<IActionResult> Create(TopicRequest request)
        {
            return Add(request);
        }
    }
}