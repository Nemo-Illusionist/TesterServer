using System.Security.Claims;
using System.Threading.Tasks;

namespace Tester.Auth.Contracts
{
    public interface ITokenProvider
    {
        Task<string> Generate(ClaimsIdentity identity);
    }
}