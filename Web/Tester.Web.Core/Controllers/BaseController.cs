using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Tester.Web.Core.Controllers
{
    [PublicAPI]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
    }   
}