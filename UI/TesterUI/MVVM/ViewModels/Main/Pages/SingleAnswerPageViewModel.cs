﻿using System;
using System.Windows.Input;
using DevExpress.Mvvm;
using TesterUI.MVVM.Models;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class SingleAnswerPageViewModel : BaseViewModel
    {
        public SingleAnswerPageViewModel()
        {
        }

        public SingleAnswerPageViewModel(SingleQuestion singleQuestion, AnswerModel answerModel)
        {
            AnswerModel = answerModel ?? throw new ArgumentNullException(nameof(answerModel));
            SingleQuestion = singleQuestion ?? throw new ArgumentNullException(nameof(singleQuestion));
        }

        public AnswerModel AnswerModel { get; set; }
        public SingleQuestion SingleQuestion { get; set; }

        public ICommand AnswerClick
        {
            get { return new DelegateCommand<string>(answer => { AnswerModel.Answer = answer; }); }
        }
    }
}