using System;

namespace Tester.Dto.User
{
    public class UserDto : BaseDto<Guid>
    {
        public string Login { get; set; }
        public string LastName { get; set; }
    }
}