using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Db.Model.Statistics;
using Tester.Dto;
using Tester.Dto.Statistic;
using Tester.Infrastructure.Contracts;
using Tester.Web.Analytics.Controllers.Base;

namespace Tester.Web.Analytics.Controllers.V1
{
    public class UserAnswerController : BaseRoController<IUserAnswerRoService, UserAnswer, Guid, UserAnswerDto, UserAnswerDto>
    {
        public UserAnswerController([NotNull] IUserAnswerRoService crudRoService,
            [NotNull] IFilterHelper filterHelper,
            [NotNull] IValidatorFactory validatorFactory)
            : base(crudRoService, filterHelper, validatorFactory)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<BaseDto<Guid>>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDto<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }
    }
}