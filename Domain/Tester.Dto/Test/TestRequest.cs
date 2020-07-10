using System;

namespace Tester.Dto.Test
{
    public class TestRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfQuestions { get; set; }
        public Guid? AuthorId { get; set; }
    }
}