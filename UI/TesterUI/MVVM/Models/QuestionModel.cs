using System;

namespace TesterUI.MVVM.Models
{
    public abstract class QuestionModel
    {
        public Guid Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string QuestionText { get; set; }
    }
}