using System;

namespace Tester.Dto.Statistic
{
    public class UserTestDto
    {
        public Guid Id { get; set; }

        public Guid TestId { get; set; }

        public BaseDto<Guid> TestAuthor { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public bool IsChecked { get; set; }
        
        public int RightAnswers { get; set; }
        
        public int WrongAnswers { get; set; }
        
        public int NumberOfQuestions { get; set; }
        
        public BaseDto<Guid> User { get; set; }

        public BaseDto<Guid> Examiner { get; set; }

        public TimeSpan? Time { get; set; }

        public bool IsOver { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}