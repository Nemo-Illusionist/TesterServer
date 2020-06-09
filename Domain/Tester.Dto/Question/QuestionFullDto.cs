using System;

namespace Tester.Dto.Question
{
    public class QuestionFullDto : QuestionDto
    {
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public BaseDto<Guid> Author { get; set; }
        public BaseDto<Guid> Topic { get; set; }
    }
}