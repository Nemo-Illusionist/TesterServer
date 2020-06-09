using System;
using AutoMapper;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Db.Model.App;
using Tester.Dto.Topic;
using Tester.Infrastructure.Contracts;

namespace Tester.Web.Admin.Services
{
    public class TopicService : BaseCrudService<Topic, Guid, TopicDto, TopicFullDto, TopicRequest>,
        ITopicService
    {
        public TopicService(IDataProvider dataProvider,
            IAsyncHelpers asyncHelpers,
            IOrderHelper orderHelper,
            IMapper mapper)
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }
    }
}