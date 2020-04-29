using Radilovsoft.Rest.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.App;
using Tester.Dto.Questions;

namespace Tester.Infrastructure.Contracts
{
    public interface IQuestionService : IBaseCrudService<Question, Guid, QuestionDto, QuestionRequest>
    {
    }
}
