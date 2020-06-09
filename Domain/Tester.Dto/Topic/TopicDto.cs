using System;

namespace Tester.Dto.Topic
{
    public class TopicDto : BaseDto<Guid>
    {
        public string Description { get; set; }
    }
}