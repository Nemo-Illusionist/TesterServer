using JetBrains.Annotations;

namespace Tester.Dto.Question
{
    [PublicAPI]
    
    public class SingleSectionQuestion
    {
        public string Answer { get; set; }
        public string[] Values { get; set; }
    }
}