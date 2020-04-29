using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Dto;
using Tester.Db.Model.App;
using Tester.Dto.Questions;
using Tester.Infrastructure.Contracts;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.V1
{
   
    public class QuestionController : BaseCrudController<IQuestionService, Question, Guid, QuestionDto, QuestionDto, QuestionRequest>
    {
        public QuestionController([NotNull] IQuestionService crudService, [NotNull] IFilterHelper filterHelper) : base(crudService, filterHelper)
    { }



    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<QuestionDto>), 200)]
    public Task<IActionResult> Get([FromQuery] FilterRequest filter)
    {
        return GetByFilter(filter);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuestionDto), 200)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public Task<IActionResult> Get(Guid id)
    {
        return GetById(id);
    }

    [HttpPost]
    [ProducesResponseType(typeof(QuestionDto), 200)]
    public Task<IActionResult> Create(QuestionRequest request)
    {
        return Add(request);
    }
}
}