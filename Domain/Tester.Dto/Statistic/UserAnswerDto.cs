using System;
using Tester.Core.Common;

namespace Tester.Dto.Statistic
{
    public class UserAnswerDto
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public BaseDto<Guid> Topic { get; set; }
        public BaseDto<Guid> Author { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Hint { get; set; }

        public QuestionType Type { get; set; }
        
        public bool? IsRight { get; set; }

        public BaseDto<Guid> UserTest { get; set; }

        public string Value { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}