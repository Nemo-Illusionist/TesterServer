using System;

namespace Tester.Dto.Test
{
    public class TestFullDto : TestDto
    {
        public BaseDto<Guid> Author { get; set; }
        public string Description { get; set; }
    }
}