using System;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    public class QuestionRequest
    {
        public Guid TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public QuestionType Type { get; set; }
        public string Answer { get; set; }
        public Guid? AuthorId { get; set; }
    }
}