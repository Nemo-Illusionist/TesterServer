using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Tester.Web.Broker.Controllers.Base;

namespace Tester.Web.Broker.Controllers.V1
{
    public class BrokerController : BaseController
    {
        public BrokerController([NotNull] IValidatorFactory validatorFactory) : base(validatorFactory)
        {
        }

        [HttpPost("{testId}")]
        public Task<IActionResult> Init(Guid testId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/next")]
        public Task<Guid> Next(Guid id, object request)
        {
            throw new NotImplementedException();
        }
    }
}