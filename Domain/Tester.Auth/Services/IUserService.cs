using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
    }
}