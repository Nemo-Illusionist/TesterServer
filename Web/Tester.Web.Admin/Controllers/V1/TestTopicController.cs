using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Dto;
using Tester.Dto.TestTopic;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
    public class TestTopicController : BaseController
    {
        private readonly IFilterHelper _filterHelper;
        private readonly ITestTopicService _service;

        public TestTopicController([NotNull] IFilterHelper filterHelper, [NotNull] ITestTopicService service)
        {
            _filterHelper = filterHelper ?? throw new ArgumentNullException(nameof(filterHelper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TestTopicDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByFilter(FilterRequest filter)
        {
            Expression<Func<TestTopicDto, bool>> expression = null;
            IPageFilter pageFilter = null;
            IOrder[] orders = null;
            if (filter != null)
            {
                if (filter.Filter != null)
                {
                    expression = _filterHelper.ToExpression<TestTopicDto>(filter.Filter);
                }

                pageFilter = filter.PageFilter ?? new PageFilter {Page = 1, PageSize = 20};
                orders = filter.Orders?.ToArray();
            }

            var result = await _service.GetByFilter(pageFilter, expression, orders)
                .ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Add(TestTopicRequest item)
        {
            await _service.Post(item).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{testId}/{topicId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid testId, Guid topicId)
        {
            try
            {
                await _service.Deleted(testId, topicId).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}