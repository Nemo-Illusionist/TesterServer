using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Db.Model.App;
using Tester.Dto.Topics;
using Tester.Infrastructure.Contracts;
using AutoMapper;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Data.Core.Contract;

namespace Tester.Web.Admin.Services
{

    public class TopicService : BaseRoService<Topic, Guid, TopicDto, TopicDto>, ITopicService
    {
        public TopicService(IRoDataProvider dataProvider, IAsyncHelpers asyncHelpers, IOrderHelper orderHelper,
            IMapper mapper,
            IDataProvider rwDataProvider)
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<Guid> Post(TopicRequest request)
        {
           
            throw new NotImplementedException();
        }

        public Task<Guid> Put(Guid id, TopicRequest request)
        {
            throw new NotImplementedException();
        }
    }
}