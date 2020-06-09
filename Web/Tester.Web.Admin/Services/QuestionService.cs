using System;
using AutoMapper;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Db.Model.App;
using Tester.Dto.Question;
using Tester.Infrastructure.Contracts;

namespace Tester.Web.Admin.Services
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
    }
}