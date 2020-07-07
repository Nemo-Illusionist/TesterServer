using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Extensions;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Auth.Extensions;
using Tester.Core.Common;
using Tester.Db.Model.App;
using Tester.Db.Model.Statistics;
using Tester.Dto.Question;
using Tester.Dto.User;
using Tester.Web.Broker.Cache;
using Tester.Web.Broker.Controllers.Base;

namespace Tester.Web.Broker.Controllers.V1
{
    public class BrokerBrokerController : BaseBrokerController
    {
        private readonly IDataProvider _dataProvider;
        private readonly ICache _cache;
        

        public BrokerBrokerController([NotNull] IDataProvider dataProvider,
            [NotNull] IValidatorFactory validatorFactory,
            ICache memoryCache)
            : base(validatorFactory)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _cache = memoryCache;
        }

        [HttpPost("{testId}")]
        public async Task<IActionResult> Init(Guid testId)
        {
            if (testId == null) throw new ArgumentNullException(nameof(testId));

            var test = await _dataProvider.GetQueryable<Test>()
                .Include(x=>x.TestTopics)
                .FirstAsync(x => x.Id == testId).ConfigureAwait(false);
            var questions = TestGenerator(test);
            var userTest = new UserTest{ IsOver = false, TestId = testId,
                UserId = User.Claims.GetUserId()};
            await _dataProvider.InsertAsync(userTest).ConfigureAwait(false);
            var key = "test_" + userTest.Id;
            await _cache.SetAsync(key,questions).ConfigureAwait(false);
            
            return Ok(new{key = userTest.Id}) ;
        }

        [HttpPost("{id}/next")]
        public async Task<IActionResult> Next(Guid id, [FromBody]UserAnswerRequest request)
        {
            //if (request.Id == null) throw new ArgumentNullException(nameof(request));
            
            var userAnswer = new UserAnswer
            {
                Id = request.Id, 
                Value = request.UserAnswer,
                UserTestId = id
            };
            await _dataProvider.InsertAsync(userAnswer).ConfigureAwait(false);
            var key = "test_" + id;
            var questions = await _cache.GetAsync<Queue<Question>>(key).ConfigureAwait(false);
            if (!questions.Any())
            {
                return NoContent();
            }
            
            var question = questions.Dequeue();
            await _cache.SetAsync(key,questions).ConfigureAwait(false);
            
            string[] answerOptions = Array.Empty<string>();
            switch (question.Type.GetDisplayName())
            {
                case "MultipleSectionQuestion":
                    answerOptions = JsonSerializer.Deserialize<MultipleSectionQuestion>(question.Answer).Values;
                  break;
                case "SingleSectionQuestion": 
                    answerOptions = JsonSerializer.Deserialize<SingleSectionQuestion>(question.Answer).Values;
                    break;
                case "OpenQuestion":
                    break;
            }

            var issueQuestion = new IssuedQuestion()
            {
                Id = question.Id,
                Name = question.Name,
                Description = question.Description,
                Hint = question.Hint,
                AnswerOptions = answerOptions
            };

            return Ok(new {issueQuestion});
        }

        private Queue<Question> TestGenerator(Test test)
        {
            try
            {
                var length = test.NumberOfQuestions;
                var questions = new Queue<Question>();
                while (length != 0)
                {
                    foreach (var topic in test.TestTopics)
                    {
                        var pickQuestion = _dataProvider.GetQueryable<Question>()
                            .Where(x => !x.DeletedUtc.HasValue)
                            .Where(x => x.Topic.Id == topic.TopicId)
                            .OrderBy(x => Guid.NewGuid()).Take(1).ToArray();
                        questions.Enqueue(pickQuestion[0]);
                    }

                    length--;
                }

                return questions;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(test));
            }
        }
    }
}