using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Auth.Extensions;
using Tester.Db.Model.App;
using Tester.Dto.Topic;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class TopicController : BaseCrudController<ITopicService, Topic, Guid, TopicDto, TopicFullDto,
        TopicRequest>
    {
        public TopicController([NotNull] ITopicService crudService,
            [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TopicDto>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status201Created)]
        public Task<IActionResult> Create([NotNull] TopicRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            request.AuthorId = User.Claims.GetUserId();
            return Add(request);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        public new Task<IActionResult> Update(Guid id, [NotNull] TopicRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            request.AuthorId = null;
            return base.Update(id, request);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public new Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}