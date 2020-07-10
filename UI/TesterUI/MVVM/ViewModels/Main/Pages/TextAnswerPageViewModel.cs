using System;
using TesterUI.MVVM.Models;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class TextAnswerPageViewModel : BaseViewModel
    {
        public TextAnswerPageViewModel()
        {
        }

        public TextAnswerPageViewModel(AnswerModel answerModel)
        {
            AnswerModel = answerModel ?? throw new ArgumentNullException(nameof(answerModel));
        }

        public AnswerModel AnswerModel { get; set; }
    }
}