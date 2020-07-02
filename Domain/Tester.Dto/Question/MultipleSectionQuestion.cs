using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class MultipleSectionQuestion
    {
        public string[] answers { get; set; }
        public string[] values { get; set; }
    }
}