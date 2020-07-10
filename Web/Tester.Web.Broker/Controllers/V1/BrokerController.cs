using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Tester.Auth.Extensions;
using Tester.Dto.Question;
using Tester.Dto.User;
using Tester.Infrastructure.Contracts;
using Tester.Web.Broker.Controllers.Base;

namespace Tester.Web.Broker.Controllers.V1
{
    public class BrokerController : BaseBrokerController
    {
        private readonly IBrokerService _brokerService;

        public BrokerController([NotNull] IValidatorFactory validatorFactory, [NotNull] IBrokerService brokerService)
            : base(validatorFactory)
        {
            _brokerService = brokerService ?? throw new ArgumentNullException(nameof(brokerService));
        }

        [HttpPost("{testId}")]
        [ProducesResponseType(typeof(BrokerResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Init(Guid testId)
        {
            if (testId == Guid.Empty) throw new ArgumentNullException(nameof(testId));

            try
            {
                var brokerResponse = await _brokerService.InitTest(testId, User.Claims.GetUserId()).ConfigureAwait(false);
                if (brokerResponse == null) return NoContent();
                return Ok(brokerResponse);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("{id}/next")]
        [ProducesResponseType(typeof(BrokerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Next(Guid id, [FromBody] UserAnswerRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            try
            {
                var brokerResponse = await _brokerService.Next(id, User.Claims.GetUserId(), request);
                if (brokerResponse == null) return NoContent();
                return Ok(brokerResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}