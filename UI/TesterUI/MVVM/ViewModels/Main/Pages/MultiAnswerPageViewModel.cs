using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using TesterUI.MVVM.Models;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class MultiAnswerPageViewModel : BaseViewModel
    {
        private readonly List<string> _answers;

        public MultiAnswerPageViewModel()
        {
        }

        public MultiAnswerPageViewModel(MultiQuestion multiQuestion, AnswerModel answerModel)
        {
            AnswerModel = answerModel ?? throw new ArgumentNullException(nameof(answerModel));
            MultiQuestion = multiQuestion ?? throw new ArgumentNullException(nameof(multiQuestion));
            _answers = new List<string>();
        }

        public AnswerModel AnswerModel { get; set; }
        public MultiQuestion MultiQuestion { get; set; }

        public ICommand AnswerClick
        {
            get
            {
                return new DelegateCommand<string>(answer =>
                {
                    if (_answers.Contains(answer))
                    {
                        _answers.Remove(answer);
                    }
                    else
                    {
                        _answers.Add(answer);
                    }

                    AnswerModel.Answer = JsonConvert.SerializeObject(_answers);
                });
            }
        }
    }
}