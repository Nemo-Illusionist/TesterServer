using System;

namespace Tester.Dto.Users
{
    public class UserDto : BaseDto<Guid>
    {
        public string Login { get; set; }
        public string LastName { get; set; }
    }
}