using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Auth.Extensions;
using Tester.Db.Model.App;
using Tester.Dto.Question;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;
using Tester.Web.Admin.Validation.Question;
using FluentValidation.Internal;
using FluentValidation.Results;
using JetBrains.Annotations;

namespace Tester.Web.Admin.Controllers.V1
{
    public class QuestionController : BaseCrudController<IQuestionService, Question, Guid, QuestionDto, QuestionFullDto,
        QuestionRequest>
    {
        public QuestionController([NotNull] IQuestionService crudService,
            [NotNull] IFilterHelper filterHelper,
            [NotNull] IValidatorFactory validatorFactory)
            : base(crudService, filterHelper, validatorFactory)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<QuestionDto>), StatusCodes.Status200OK)]
        public Task<IActionResult> Get([FromQuery] FilterRequest filter)
        {
            return GetByFilter(filter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(Guid id)
        {
            return GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([NotNull] QuestionRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var validator = ValidatorFactory.GetValidator<QuestionRequest>();
            try
            {
                await validator.ValidateAndThrowAsync(request).ConfigureAwait(false);
                request.AuthorId = User.Claims.GetUserId();
                return await Add(request).ConfigureAwait(false);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public new Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}