using System;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.App;
using Tester.Dto.Topic;

namespace Tester.Infrastructure.Contracts
{
    public interface ITopicService : IBaseCrudService<Topic, Guid, TopicDto, TopicFullDto, TopicRequest>
    {
    }
}