using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.Dto.Role
{
    class RoleDto: BaseDto<Guid>
    {
        public string Login { get; set; }
        public string LastName { get; set; }
    }
}
