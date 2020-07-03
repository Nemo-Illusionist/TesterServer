using System;
using System.Threading.Tasks;
using AutoMapper;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Db.Model.App;
using Tester.Dto.Question;
using Tester.Infrastructure.Contracts;

namespace Tester.Infrastructure.Services
{
    public class QuestionService : BaseCrudService<Question, Guid, QuestionDto, QuestionFullDto, QuestionRequest>,
        IQuestionService
    {
        public QuestionService(IDataProvider dataProvider,
            IAsyncHelpers asyncHelpers,
            IOrderHelper orderHelper,
            IMapper mapper)
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }

        public override Task<Guid> Put(Guid id, QuestionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}