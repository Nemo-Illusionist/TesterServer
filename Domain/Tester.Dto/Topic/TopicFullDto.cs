using System;
using JetBrains.Annotations;

namespace Tester.Dto.Topic
{
    [PublicAPI]
    public class TopicFullDto : TopicDto
    {
        public BaseDto<Guid> Author { get; set; }
        public string Description { get; set; }
    }
}