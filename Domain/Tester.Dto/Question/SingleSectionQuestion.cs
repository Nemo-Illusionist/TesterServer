using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class SingleSectionQuestion
    {
        public string answer { get; set; }
        public string[] values { get; set; }
    }
}