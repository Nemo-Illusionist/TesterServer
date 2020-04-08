using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.Dto.Users
{
    public class UserDto: BaseDto<Guid>
    {
        public string Login { get; set; }
        public string LastName { get; set; }
    }
}
