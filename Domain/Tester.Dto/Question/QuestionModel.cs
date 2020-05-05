using Tester.Core.Common;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.Dto.Question
{
    public class QuestionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public string AuthorId { get; set; }
        public QuestionType Type { get; set; }
        public TopicModel Topic { get; set; }
    }
}