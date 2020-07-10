using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Core.Common;
using Tester.Db.Model.App;
using Tester.Db.Model.Statistics;
using Tester.Dto.Question;
using Tester.Dto.User;
using Tester.Infrastructure.Contracts;
using Tester.Infrastructure.Services.Cache;

namespace Tester.Infrastructure.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IDataProvider _dataProvider;
        private readonly ICache _cache;

        public BrokerService([NotNull] ICache cache, [NotNull] IDataProvider dataProvider)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public async Task<BrokerResponse> InitTest(Guid testId, Guid userId)
        {
            if (testId == null) throw new ArgumentNullException(nameof(testId));

            var test = await _dataProvider.GetQueryable<Test>()
                .Include(x => x.TestTopics)
                .FirstAsync(x => x.Id == testId).ConfigureAwait(false);
            var questions = await TestGenerator(test).ConfigureAwait(false);
            var userTest = new UserTest
            {
                IsOver = false, TestId = testId,
                UserId = userId
            };
            await _dataProvider.InsertAsync(userTest).ConfigureAwait(false);
            var key = "test_" + userTest.Id;
            await _cache.SetAsync(key, questions).ConfigureAwait(false);
            var issueQuestion = IssuedQuestion(questions.Peek());

            return new BrokerResponse {Key = userTest.Id, Question = issueQuestion, Count = questions.Count};
        }

        public async Task<BrokerResponse> Next(Guid id, Guid userId, UserAnswerRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var any = await _dataProvider.GetQueryable<UserTest>().AnyAsync(x => x.UserId == userId && x.Id == id).ConfigureAwait(false);
            if (!any) throw new ItemNotFoundException();

            var key = "test_" + id;
            var questions = await _cache.GetAsync<Queue<Question>>(key).ConfigureAwait(false);

            var question = questions.Peek();
            if (question.Id != request.Id) throw new ArgumentException(nameof(request.Id));
            
           
            var userAnswer = new UserAnswer
            {
                QuestionId = request.Id,
                Value = JsonSerializer.Serialize(request.UserAnswer),
                UserTestId = id
            };
            await _dataProvider.InsertAsync(userAnswer).ConfigureAwait(false);

            questions.Dequeue();
            if (!questions.Any())
            {
                var test = await _dataProvider.GetQueryable<UserTest>().Where(x=>x.Id == id).FirstAsync().ConfigureAwait(false);
                test.IsOver = true;
                await _dataProvider.UpdateAsync(test).ConfigureAwait(false);
              
                return null;
            }

            question = questions.Peek();
            await _cache.SetAsync(key, questions).ConfigureAwait(false);

            var issueQuestion = IssuedQuestion(question);

            return new BrokerResponse {Key = id, Question = issueQuestion, Count = questions.Count};
        }

        private static IssuedQuestion IssuedQuestion(Question question)
        {
            var answerOptions = Array.Empty<string>();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var issueQuestion = new IssuedQuestion
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
            int count = test.NumberOfQuestions / test.TestTopics.Count + 1;;
            int addition = test.NumberOfQuestions % test.TestTopics.Count;
            int take = 0;
            var questions = new Queue<Question>();
            
            
            foreach (var topic in test.TestTopics)
            {
                if (addition != 0)
                {
                    take = count + 1;
                    addition--;
                }
                else
                {
                    take = count;
                }
                var pickQuestion = await _dataProvider.GetQueryable<Question>()
                    .Where(x => !x.DeletedUtc.HasValue && x.TopicId == topic.TopicId )
                    .OrderBy(x => Guid.NewGuid()).Take(take).FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (pickQuestion != null)
                {
                    questions.Enqueue(pickQuestion);
                }
            }
              
                


            return questions;
        }
    }
}