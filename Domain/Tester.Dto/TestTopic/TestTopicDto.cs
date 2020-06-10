using System;
using JetBrains.Annotations;

namespace Tester.Dto.TestTopic
{
    [PublicAPI]
    public class TestTopicDto
    {
        public BaseDto<Guid> Test { get; set; }
        public BaseDto<Guid> Topic { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }
}