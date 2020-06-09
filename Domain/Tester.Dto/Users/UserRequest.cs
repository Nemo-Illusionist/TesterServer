using JetBrains.Annotations;

namespace Tester.Dto.Users
{
    [PublicAPI]
    public class UserRequest
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}