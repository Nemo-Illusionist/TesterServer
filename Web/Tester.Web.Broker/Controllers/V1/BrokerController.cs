using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BrokerController : BaseBrokerController
    {
        private readonly IDataProvider _dataProvider;
        private readonly ICache _cache;


        public BrokerController([NotNull] IDataProvider dataProvider,
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
                .Include(x => x.TestTopics)
                .FirstAsync(x => x.Id == testId).ConfigureAwait(false);
            var questions = await TestGenerator(test).ConfigureAwait(false);
            var userTest = new UserTest
            {
                IsOver = false, TestId = testId,
                UserId = User.Claims.GetUserId()
            };
            await _dataProvider.InsertAsync(userTest).ConfigureAwait(false);
            var key = "test_" + userTest.Id;
            await _cache.SetAsync(key, questions).ConfigureAwait(false);
            var issueQuestion = IssuedQuestion(questions.Peek());

            return Ok(new {key = userTest.Id, question = issueQuestion});
        }

        [HttpPost("{id}/next")]
        public async Task<IActionResult> Next(Guid id, [FromBody] UserAnswerRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var userAnswer = new UserAnswer
            {
                QuestionId = request.Id,
                Value = JsonSerializer.Serialize(request.UserAnswer),
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
            await _cache.SetAsync(key, questions).ConfigureAwait(false);

            var issueQuestion = IssuedQuestion(question);

            return Ok(new {key = id, question = issueQuestion});
        }

        private static IssuedQuestion IssuedQuestion(Question question)
        {
            string[] answerOptions = Array.Empty<string>();
            switch (question.Type)
            {
                case QuestionType.MultipleSelection:
                    answerOptions = JsonSerializer.Deserialize<MultipleSectionQuestion>(question.Answer).Values;
                    break;
                case QuestionType.SingleSelection:
                    answerOptions = JsonSerializer.Deserialize<SingleSectionQuestion>(question.Answer).Values;
                    break;
                case QuestionType.Open:
                    break;
            }

            var issueQuestion = new IssuedQuestion()
            {
                Id = question.Id,
                Name = question.Name,
                Description = question.Description,
                Hint = question.Hint,
                Type = question.Type,
                AnswerOptions = answerOptions
            };
            return issueQuestion;
        }

        private async Task<Queue<Question>> TestGenerator(Test test)
        {
            var length = test.NumberOfQuestions;
            var questions = new Queue<Question>();
            while (length != 0)
            {
                foreach (var topic in test.TestTopics)
                {
                    var pickQuestion = await _dataProvider.GetQueryable<Question>()
                        .Where(x => !x.DeletedUtc.HasValue && x.TopicId == topic.TopicId)
                        .OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (pickQuestion != null)
                    {
                        questions.Enqueue(pickQuestion);
                        length--;
                        if (length == 0) break;
                    }
                }
            }

            return questions;
        }
    }
}