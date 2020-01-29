using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Tester.Web.Admin.Controllers.Base
{
    [PublicAPI]
    [Produces("application/json")]
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
    }
}