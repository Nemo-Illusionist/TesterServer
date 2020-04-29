using System;
using System.Collections.Generic;
using System.Text;
using Tester.Core.Common;

namespace Tester.Dto.Users
{
    public class UserRequest
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
    }
}
