using System;
using System.Threading.Tasks;
using Tester.Dto.Question;
using Tester.Dto.User;

namespace Tester.Infrastructure.Contracts
{
    public interface IBrokerService
    {
        Task<BrokerResponse> InitTest(Guid testId, Guid userId);
        Task<BrokerResponse> Next(Guid id, Guid userId, UserAnswerRequest request);
    }
}