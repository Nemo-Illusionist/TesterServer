using System.Threading.Tasks;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using REST.DataCore.Contract;

namespace Tester.Web.Admin.Controllers.Base
{
    public class BaseV1Controller : BaseController
    {
        private readonly UserService _auth;
        protected async Task<IActionResult> Authorization(string Login, string Password)
        {
            var token = await _auth.Authenticate(Login, Password).ConfigureAwait(false);
        }
    }
}