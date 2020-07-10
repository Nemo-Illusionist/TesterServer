using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using JetBrains.Annotations;
using Tester.Dto.Question;
using TesterUI.MVVM.Models;
using TesterUI.MVVM.VIews.Main.Pages;
using OpenQuestion = TesterUI.MVVM.Models.OpenQuestion;
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
            if (test == null) throw new ArgumentNullException(nameof(test));

            Init(test);
        }

        private void Init(TestModel question)
        {
            AppContext = App.Context;
            Answer = new AnswerModel();
            Question = GetFirstQuestion(question.Id).Result;
            SetAnswerPage(Question);
        }

        private async Task<QuestionModel> GetFirstQuestion(Guid testId)
        {
            var response = await AppContext.BrokerApi.GetQuestionByTestId(testId).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return Map(responseContent);
            }
            else
            {
                MessageBox.Show(response.Error.Content);
            }

            //todo исправить
            return null;
        }

        private async Task<QuestionModel> GetNextQuestion(Guid keyId)
        {
            var response = await AppContext.BrokerApi.GetNextQuestion(keyId).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return Map(responseContent);
            }
            else
            {
                MessageBox.Show(response.Error.Content);
            }

            //todo исправить
            return null;
        }


        private static QuestionModel Map(BrokerResponse brokerResponse)
        {
            var question = brokerResponse.Question;
            switch (question.Type)
            {
                case QuestionType.Open:
                    return new OpenQuestion
                    {
                        QuestionText = question.Description,
                        Answers = question.AnswerOptions.Select(i => new AnswerModel
                        {
                            Key = brokerResponse.Key,
                            Answer = i
                        }).ToArray(),
                        Key = brokerResponse.Key,
                    };
                case QuestionType.MultipleSelection:
                    return new TextQuestion
                    {
                        QuestionText = question.Description,
                        Key = brokerResponse.Key,
                    };
                case QuestionType.SingleSelection:
                    return new SingleQuestion
                    {
                        QuestionText = question.Description,
                        Answers = question.AnswerOptions.Select(i => new AnswerModel
                        {
                            Key = brokerResponse.Key,
                            Answer = i
                        }).ToArray(),
                        Key = brokerResponse.Key,
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
                    Question = await GetNextQuestion(Question.Key).ConfigureAwait(true);
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
            else if (question is OpenQuestion open)
            {
                AnswerPage = new OpenAnswerPage()
                {
                    DataContext = new OpenAnswerPageViewModel(open, Answer)
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