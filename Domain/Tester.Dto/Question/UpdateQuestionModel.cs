using System;
using System.ComponentModel.DataAnnotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    public class UpdateQuestionModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public QuestionType Type { get; set; }
        public Guid TopicId { get; set; }
    }
}