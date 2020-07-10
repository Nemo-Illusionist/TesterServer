using System;
using System.Threading.Tasks;
using Radilovsoft.Rest.Infrastructure.Dto;
using Refit;
using Tester.Dto;
using Tester.Dto.Question;
using Tester.Dto.Test;
using Tester.Dto.User;

namespace TesterUI.Helpers.Api
{
    public interface IBrokerApi
    {
        [Post("/api/v1/Broker/{testId}")]
        Task<ApiResponse<BrokerResponse>> GetQuestionByTestId(Guid testId);

        [Post("/api/v1/Broker/{id}/next")]
        Task<ApiResponse<BrokerResponse>> GetNextQuestion(Guid id, [Body] UserAnswerRequest answer);

        [Get("/api/v1/Test")]
        Task<PagedResult<TestFullDto>> SearchTests([Query] FilterRequest filter);
    }
}