using System;
using TesterUI.MVVM.Models;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class OpenAnswerPageViewModel : BaseViewModel
    {
        public OpenAnswerPageViewModel()
        {
        }

        public OpenAnswerPageViewModel(OpenQuestion openQuestion, AnswerModel answerModel)
        {
            AnswerModel = answerModel ?? throw new ArgumentNullException(nameof(answerModel));
            OpenQuestion = openQuestion ?? throw new ArgumentNullException(nameof(openQuestion));
        }

        public AnswerModel AnswerModel { get; set; }
        public OpenQuestion OpenQuestion { get; set; }
    }
}