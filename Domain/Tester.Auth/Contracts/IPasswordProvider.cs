using Tester.Auth.Models;

namespace Tester.Auth.Contracts
{
    public interface IPasswordProvider
    {
        PasswordHash CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, PasswordHash hash);
    }
}