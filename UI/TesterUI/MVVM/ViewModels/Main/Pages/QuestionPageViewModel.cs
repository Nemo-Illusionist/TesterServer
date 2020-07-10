using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using JetBrains.Annotations;
using Tester.Dto.Question;
using Tester.Dto.User;
using TesterUI.Helpers.WpfExtensions;
using TesterUI.MVVM.Models;
using TesterUI.MVVM.VIews.Main.Pages;
using QuestionType = Tester.Core.Common.QuestionType;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class QuestionPageViewModel : BaseViewModel
    {
        public Context AppContext { get; set; }

        public QuestionPageViewModel()
        {
        }

        public QuestionPageViewModel([NotNull] TestModel test)
        {
            Test = test ?? throw new ArgumentNullException(nameof(test));

            Init();
        }

        private void Init()
        {
            AppContext = App.Context;
            Answer = new AnswerModel();
            Question = GetFirstQuestion().Result;
            SetAnswerPage(Question);
        }

        private async Task<QuestionModel> GetFirstQuestion()
        {
            var response = await AppContext.BrokerApi.GetQuestionByTestId(Test.Id).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return Map(responseContent);
            }
            else
            {
                MessageBox.Show(response.Error.Content);
                throw new Exception(response.Error.Content);
            }
        }

        private async Task<QuestionModel> GetNextQuestion()
        {
            var response = await AppContext.BrokerApi.GetNextQuestion(Question.Key, new UserAnswerRequest
            {
                Id = Question.Id,
                UserAnswer = Answer.Answer
            }).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return Map(responseContent);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }
            else
            {
                MessageBox.Show(response.Error.Content);
                throw new Exception(response.Error.Content);
            }
        }


        private static QuestionModel Map(BrokerResponse brokerResponse)
        {
            if (brokerResponse == null) return null;

            var question = brokerResponse.Question;
            switch (question.Type)
            {
                case QuestionType.MultipleSelection:
                    return new MultiQuestion
                    {
                        Id = question.Id,
                        Key = brokerResponse.Key,
                        Name = question.Name,
                        QuestionText = question.Description,
                        Answers = question.AnswerOptions.Select(i => new AnswerModel
                        {
                            Answer = i
                        }).ToArray(),
                    };
                case QuestionType.Open:
                    return new TextQuestion
                    {
                        Id = question.Id,
                        Name = question.Name,
                        Key = brokerResponse.Key,
                        QuestionText = question.Description,
                    };
                case QuestionType.SingleSelection:
                    return new SingleQuestion
                    {
                        Id = question.Id,
                        Name = question.Name,
                        QuestionText = question.Description,
                        Key = brokerResponse.Key,
                        Answers = question.AnswerOptions.Select(i => new AnswerModel
                        {
                            Answer = i
                        }).ToArray(),
                    };
                case QuestionType.OrderedList:
                case QuestionType.Conformity:
                default:
                    throw new ArgumentOutOfRangeException(question.Type.ToString());
            }
        }

        public TestModel Test { get; set; }
        public QuestionModel Question { get; set; }

        public AnswerModel Answer { get; set; }

        public Page AnswerPage { get; set; }

        public ICommand NextQuestionClick
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (string.IsNullOrWhiteSpace(Answer.Answer) || Answer.Answer == "[]")
                    {
                        var box = await AppContext.MainDialog.ShowDialog(new DialogModel("Внимание", "Вы уверены что хотите оставить пустой ответ?", DialogType.YesNot)).ConfigureAwait(true);
                        if (box != ResultDialogType.Yes)
                        {
                            return;
                        }
                    }

                    Question = await GetNextQuestion().ConfigureAwait(true);

                    if (Question == null)
                    {
                        await AppContext.MainDialog.ShowDialog(new DialogModel("Внимание", "Тест пройден", DialogType.Ok)).ConfigureAwait(true);
                        AppContext.SetPage(new ProfilePage());
                        return;
                    }

                    Answer.Answer = string.Empty;
                    SetAnswerPage(Question);
                });
            }
        }

        private void SetAnswerPage(QuestionModel question)
        {
            if (question is SingleQuestion single)
            {
                AnswerPage = new SingleAnswerPage()
                {
                    DataContext = new SingleAnswerPageViewModel(single, Answer)
                };
            }
            else if (question is MultiQuestion open)
            {
                AnswerPage = new MultiAnswerPage()
                {
                    DataContext = new MultiAnswerPageViewModel(open, Answer)
                };
            }
            else if (question is TextQuestion)
            {
                AnswerPage = new TextAnswerPage()
                {
                    DataContext = new TextAnswerPageViewModel(Answer)
                };
            }
        }
    }
}