using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tester.Core.Constant;

namespace Tester.Web.Broker.Controllers.Base
{
    [PublicAPI]
    [Produces("application/json")]
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNameConstant.Student)]
    public abstract class BaseController : Controller
    {
        protected IValidatorFactory ValidatorFactory { get; }

        protected BaseController([NotNull] IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

    }   
}