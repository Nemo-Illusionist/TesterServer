using System;
using JetBrains.Annotations;

namespace Tester.Dto.Topic
{
    [PublicAPI]
    public class TopicRequest
    {
        public Guid? ParentId { get; set; }
        public Guid? AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}