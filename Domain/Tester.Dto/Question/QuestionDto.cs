using System;
using Tester.Core.Common;
using Tester.Dto.Topic;

namespace Tester.Dto.Question
{
    public class QuestionDto : BaseDto<Guid>
    {
        public QuestionType Type { get; set; }
    }

    public class QuestionFullDto : QuestionDto
    {
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public BaseDto<Guid> Author { get; set; }
        public TopicDto Topic { get; set; }
    }
}