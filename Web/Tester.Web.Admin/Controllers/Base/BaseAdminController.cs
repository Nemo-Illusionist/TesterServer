using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tester.Web.Admin.Constant;
using Tester.Web.Core.Controllers;

namespace Tester.Web.Admin.Controllers.Base
{
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = AdminAccessRoles.Roles)]
    public abstract class BaseAdminController : BaseController
    {
        protected IValidatorFactory ValidatorFactory { get; }

        protected BaseAdminController([NotNull] IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

    }   
}