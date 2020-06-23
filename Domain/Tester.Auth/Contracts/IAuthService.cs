using System.Threading.Tasks;

namespace Tester.Auth.Contracts
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password, params string[] role);
    }
}