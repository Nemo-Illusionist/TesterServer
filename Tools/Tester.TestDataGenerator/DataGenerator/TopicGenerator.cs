using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.TestDataGenerator.DataGenerator
{
    public static class TopicGenerator
    {
        public static async Task<IEnumerable<Topic>> Gen([NotNull] IDataProvider dataProvider, IEnumerable<User> users)
        {
            if (dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));
            var faker = new Faker<Topic>()
                .RuleFor(x => x.AuthorId, (f, u) => f.PickRandom(users).Id)
                .RuleFor(x => x.Name, (f, u) => f.Name.JobTitle())
                .RuleFor(x => x.Description, (f, u) => f.Name.JobDescriptor());
            var topics = new List<Topic>();
            for (int i = 0; i < 10; i++)
            {
                var topic = faker.Generate();
                await dataProvider.InsertAsync(topic).ConfigureAwait(false);
                topics.Add(topic);
                for (int j = 0; j < 100; j++)
                {
                    var subTopic = faker.Generate();
                    subTopic.Parent = topic;
                    subTopic.ParentId = topic.Id;
                    topics.Add(subTopic);
                }
            }

            return topics;
        }
    }
}