using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Tester.Core.Common;
using Tester.Dto.Users;

namespace Tester.Dto.Question
{
    public class CreateQuestionModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        public string Topic { get; set; }
    }
}