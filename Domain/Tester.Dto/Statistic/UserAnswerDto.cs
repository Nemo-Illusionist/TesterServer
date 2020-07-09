using System;

namespace Tester.Dto.Statistic
{
    public class UserAnswerDto
    {
        public Guid Id { get; set; }

        public BaseDto<Guid> UserTest { get; set; }

        public BaseDto<Guid> Question { get; set; }

        public string Value { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}