using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    public class MultipleSectionQuestion
    {
        public string[] Answers { get; set; }
        public string[] Values { get; set; }
    }
}