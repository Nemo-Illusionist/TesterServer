using System.ComponentModel.DataAnnotations;

namespace Tester.Dto.User
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}