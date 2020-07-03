using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.TestDataGenerator.DataGenerator
{
    public static class TestGenerator
    {
        public static async Task<IEnumerable<Test>> Gen([NotNull] IDataProvider dataProvider,
            IEnumerable<User> users,
            IEnumerable<Topic> topics)
        {
            if (dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));

            var faker = new Faker<Test>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, (f, u) => f.Name.JobTitle())
                .RuleFor(x => x.Description, (f, u) => f.Name.JobDescriptor())
                .RuleFor(x => x.NumberOfQuestions, (f, u) => f.Random.Int(20, 50))
                .RuleFor(x => x.AuthorId, (f, u) => f.PickRandom(users).Id);

            var tests = new List<Test>();
            for (int i = 0; i < 100; i++)
            {
                var test = faker.Generate();
                await dataProvider.InsertAsync(test).ConfigureAwait(false);

                test.TestTopics = new Faker().PickRandom(topics, 10)
                    .Select(x => new TestTopic {TestId = test.Id, TopicId = x.Id})
                    .ToArray();
                await dataProvider.BatchInsertAsync(test.TestTopics).ConfigureAwait(false);

                tests.Add(test);
            }

            return tests;
        }
    }
}