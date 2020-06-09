using System;

namespace Tester.Dto.Test
{
    public class TestDto : BaseDto<Guid>
    {
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }
}