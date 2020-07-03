using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Tester.Web.Core.Controllers;

namespace Tester.Web.Broker.Controllers.Base
{
    [Authorize]
    public abstract class BaseBrokerController : BaseController
    {
        protected IValidatorFactory ValidatorFactory { get; }

        protected BaseBrokerController([NotNull] IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }
    }
}