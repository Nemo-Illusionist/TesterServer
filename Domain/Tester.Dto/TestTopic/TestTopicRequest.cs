using System;
using JetBrains.Annotations;

namespace Tester.Dto.TestTopic
{
    [PublicAPI]
    public class TestTopicRequest
    {
        public Guid TestId { get; set; }
        public Guid TopicId { get; set; }
    }
}