using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Dto.TestTopic;
using Tester.Infrastructure.Contracts;

namespace Tester.Web.Admin.Services
{
    public class TestTopicService : ITestTopicService
    {
        public Task<PagedResult<TestTopicDto>> GetByFilter(IPageFilter pageFilter, Expression<Func<TestTopicDto, bool>> filter = null, IOrder[] orders = null)
        {
            throw new NotImplementedException();
        }

        public Task Post(TestTopicRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Deleted(Guid testId, Guid topicId)
        {
            throw new NotImplementedException();
        }
    }
}