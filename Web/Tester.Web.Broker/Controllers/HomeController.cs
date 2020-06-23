using Microsoft.AspNetCore.Mvc;

namespace Tester.Web.Broker.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Base method
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}