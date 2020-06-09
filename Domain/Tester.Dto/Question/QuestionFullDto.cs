using System;
using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    public class QuestionFullDto : QuestionDto
    {
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public BaseDto<Guid> Author { get; set; }
        public BaseDto<Guid> Topic { get; set; }
    }
}