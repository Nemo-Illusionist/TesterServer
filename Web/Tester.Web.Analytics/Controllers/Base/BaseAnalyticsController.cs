using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Tester.Web.Core.Controllers;

namespace Tester.Web.Analytics.Controllers.Base
{
    [Authorize]
    public abstract class BaseAnalyticsController : BaseController
    {
        protected IValidatorFactory ValidatorFactory { get; }

        protected BaseAnalyticsController([NotNull] IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }
    }
}