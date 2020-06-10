using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Dto.TestTopic;

namespace Tester.Infrastructure.Contracts
{
    public interface ITestTopicService
    {
        Task<PagedResult<TestTopicDto>> GetByFilter(
            IPageFilter pageFilter,
            Expression<Func<TestTopicDto, bool>> filter = null,
            IOrder[] orders = null);
        
        Task Post(TestTopicRequest request);

        Task Deleted(Guid testId, Guid topicId);
    }
}