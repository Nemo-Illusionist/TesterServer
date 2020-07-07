using System;
using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class IssuedQuestion
    {
        
        public Guid Id { get; set; }

        public string  Name { get; set; }
        
        public string Description { get; set; }

        public string Hint { get; set; }
        
        public string[] AnswerOptions { get; set; }
    }
}