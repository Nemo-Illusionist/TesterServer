using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tester.Web.Admin.Constant;

namespace Tester.Web.Admin.Controllers.Base
{
    [PublicAPI]
    [Produces("application/json")]
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = AdminAccessRoles.Roles)]
    public abstract class BaseController : Controller
    {
        protected IValidatorFactory ValidatorFactory { get; }

        protected BaseController([NotNull] IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

    }   
}