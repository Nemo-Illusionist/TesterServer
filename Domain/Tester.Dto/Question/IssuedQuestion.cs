using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Tester.Core.Common;

namespace Tester.Dto.Question
{
    [PublicAPI]
    public class IssuedQuestion
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Hint { get; set; }

        public QuestionType Type { get; set; }

        public IEnumerable<string> AnswerOptions { get; set; }
    }
}