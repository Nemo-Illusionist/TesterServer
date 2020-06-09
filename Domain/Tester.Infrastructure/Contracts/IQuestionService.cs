using System;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.App;
using Tester.Dto.Question;

namespace Tester.Infrastructure.Contracts
{
    public interface IQuestionService : IBaseCrudService<Question, Guid, QuestionDto, QuestionFullDto, QuestionRequest>
    {
    }
}