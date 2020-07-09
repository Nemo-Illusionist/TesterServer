using System;

namespace Tester.Dto.Statistic
{
    public class UserTestDto
    {
        public Guid Id { get; set; }

        public BaseDto<Guid> Test { get; set; }
        public BaseDto<Guid> User { get; set; }

        public BaseDto<Guid> Examiner { get; set; }

        public TimeSpan? Time { get; set; }

        public bool IsOver { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}