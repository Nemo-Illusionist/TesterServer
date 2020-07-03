using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Db.Model.App;
using Tester.Web.Broker.Controllers.Base;

namespace Tester.Web.Broker.Controllers.V1
{
    public class BrokerBrokerController : BaseBrokerController
    {
        private readonly IDataProvider _dataProvider;
        private IMemoryCache _cache;

        public BrokerBrokerController([NotNull] IDataProvider dataProvider,
            [NotNull] IValidatorFactory validatorFactory,
            IMemoryCache memoryCache)
            : base(validatorFactory)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _cache = memoryCache;
        }

        [HttpPost("{testId}")]
        public Task<IActionResult> Init(Guid testId)
        {
            if (testId == null) throw new ArgumentNullException(nameof(testId));

            var test = _dataProvider.GetQueryable<Test>().Include(x=>x.TestTopics).First(x => x.Id == testId);
            var questions = TestGenerator(test);
            foreach (var question in questions)
            {
                _cache.Set<Question>("test_" + question.Id, question);
            }
            //UserTest userTest = new UserTest();

            // TestRequest request = new TestRequest();
            // userTest.User = service... 


            return null;
        }

        [HttpGet("{id}/next")]
        public Task<Guid> Next(Guid id, object request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            throw new NotImplementedException();
        }

        private Question[] TestGenerator(Test test)
        {
            try
            {
                var length = test.NumberOfQuestions;
                Question[] questions = new Question[length];
                while (length != 0)
                {
                    foreach (var topic in test.TestTopics)
                    {
                        var _questions = _dataProvider.GetQueryable<Question>().Where(x => !x.DeletedUtc.HasValue)
                            .Where(x => x.Topic.Id == topic.TopicId)
                            .OrderBy(x => Guid.NewGuid()).Take(1).ToArray();
                        // questions.Append<>(_questions);
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