using System.Security.Claims;

namespace Tester.Auth.Contracts
{
    public interface ITokenProvider
    {
        string Generate(ClaimsIdentity identity);
    }
}