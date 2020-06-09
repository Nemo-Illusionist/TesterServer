using System;
using JetBrains.Annotations;

namespace Tester.Dto.Topic
{
    [PublicAPI]
    public class TopicDto : BaseDto<Guid>
    {
        public BaseDto<Guid> Parent { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }
}