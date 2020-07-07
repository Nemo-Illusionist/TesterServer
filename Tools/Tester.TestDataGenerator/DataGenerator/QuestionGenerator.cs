using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Core.Common;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;
using Tester.Dto.Question;

namespace Tester.TestDataGenerator.DataGenerator
{
    public static class QuestionGenerator
    {
        public static async Task<IEnumerable<Question>> Gen([NotNull] IDataProvider dataProvider,
            IEnumerable<User> users,
            IEnumerable<Topic> topics)
        {
            if (dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));
            var faker = new Faker<Question>()
                .RuleFor(x => x.AuthorId, (f, u) => f.PickRandom(users).Id)
                .RuleFor(x => x.Name, (f, u) => f.Name.JobTitle())
                .RuleFor(x => x.Description, (f, u) => f.Name.JobDescriptor())
                .RuleFor(x => x.Hint, (f, u) => f.Name.JobDescriptor());
            var questions = new List<Question>();
            foreach (var topic in topics)
            {
                for (int i = 0; i < 3; i++)
                {
                    var question = faker.Generate();
                    question.TopicId = topic.Id;
                    question.Type = QuestionType.Open;
                    question.Answer = JsonConvert.SerializeObject(new OpenQuestion {Answers = new[] {$"answer{i}"}});
                    questions.Add(question);
                }

                for (int i = 0; i < 3; i++)
                {
                    var question = faker.Generate();
                    question.TopicId = topic.Id;
                    question.Type = QuestionType.MultipleSelection;
                    question.Answer = JsonConvert.SerializeObject(new MultipleSectionQuestion
                    {
                        Values = new[] {$"answer{i}", $"answer{i + 1}", $"answer{i + 2}"},
                        Answers = new[] {$"answer{i}", $"answer{i + 1}"}
                    });
                    questions.Add(question);
                }

                for (int i = 0; i < 3; i++)
                {
                    var question = faker.Generate();
                    question.TopicId = topic.Id;
                    question.Type = QuestionType.SingleSelection;
                    question.Answer = JsonConvert.SerializeObject(new SingleSectionQuestion
                    {
                        Values = new[] {$"answer{i}", $"answer{i + 1}", $"answer{i + 2}"},
                        Answer = $"answer{i + 1}"
                    });
                    questions.Add(question);
                }
            }

            await dataProvider.BatchInsertAsync(questions).ConfigureAwait(false);

            return questions;
        }
    }
}