using Radilovsoft.Rest.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.App;
using Tester.Dto.Topics;

namespace Tester.Infrastructure.Contracts
{
    public interface ITopicService : IBaseCrudService<Topic, Guid, TopicDto, TopicRequest>
    {

    }
}
