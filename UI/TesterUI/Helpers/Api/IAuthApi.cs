using System.Threading.Tasks;
using Refit;
using Tester.Dto.User;
using TesterUI.MVVM.Models;

namespace TesterUI.Helpers.Api
{
    public interface IAuthApi
    {
        [Post("/api/v1/Authorization")]
        Task<ApiResponse<TokenDto>> Login([Body] UserModel request);
    }
}