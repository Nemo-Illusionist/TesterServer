using System;

namespace TesterUI.MVVM.Models
{
    public abstract class QuestionModel
    {
        public Guid Key { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}