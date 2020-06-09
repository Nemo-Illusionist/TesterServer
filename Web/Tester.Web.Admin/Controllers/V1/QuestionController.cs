using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Core.Exception;
using Radilovsoft.Rest.Data.Core.Contract;
using Tester.Db.Model.App;
using Tester.Dto;
using Tester.Dto.Question;
using Tester.Web.Admin.Controllers.Base;
using Tester.Web.Admin.Services;

namespace Tester.Web.Admin.Controllers.V1
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        private readonly IAsyncHelpers _asyncHelpers;

        public QuestionController([NotNull] IQuestionService questionService, 
            IMapper mapper, 
            [NotNull] IAsyncHelpers asyncHelpers)
        {
            _asyncHelpers = asyncHelpers ?? throw new ArgumentNullException(nameof(asyncHelpers));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _questionService = questionService ?? throw new ArgumentNullException(nameof(_questionService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        public async Task<ActionResult<IEnumerable<QuestionModel>>> GetAll()
        {
            var query = _questionService.GetAll();
            return await _mapper.ProjectTo<QuestionModel>(query).ToArrayAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        public async Task<ActionResult<QuestionModel>> GetById(Guid id)
        {
            var query = _questionService.GetById(id);
            var res = await query.ToArrayAsync();
            return await _mapper.ProjectTo<QuestionModel>(query).FirstOrDefaultAsync();
        }
        
        [HttpGet("topic/{topicId}")]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<IEnumerable<QuestionModel>>> GetByTopicId(Guid topicId)
        {
            try
            {
                var query = _questionService.GetAllByTopicId(topicId);
                return await _mapper.ProjectTo<QuestionModel>(query).ToArrayAsync();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionModel>> Create(CreateQuestionModel model)
        {
            try
            {
                var question = _mapper.Map<Question>(model);
                
                
                // var question = await _asyncHelpers.FirstOrDefaultAsync<Question>(query).ConfigureAwait(false);
                return Ok(_questionService.Create(question));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] UpdateQuestionModel model)
        {
            try
            {
                var query = _mapper.Map<Question>(model);
                _questionService.Update(query);   
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _questionService.Delete(id).ConfigureAwait(false);
                return Ok();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}