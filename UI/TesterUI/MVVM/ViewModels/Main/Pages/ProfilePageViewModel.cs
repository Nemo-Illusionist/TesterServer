using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Tester.Dto;
using TesterUI.MVVM.Models;
using TesterUI.MVVM.VIews.Auth.Pages;
using TesterUI.MVVM.VIews.Main.Pages;

namespace TesterUI.MVVM.ViewModels.Main.Pages
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public Context AppContext { get; set; }

        public ProfilePageViewModel()
        {
            Init();
        }

        private void Init()
        {
            AppContext = App.Context;
            InitTests().ReturnSuccess();
        }

        private async Task InitTests()
        {
            Tests = new ObservableCollection<TestModel>();

            var response = await AppContext.BrokerApi.SearchTests(new FilterRequest
            {
                PageFilter = new PageFilter
                {
                    Page = 1,
                    PageSize = 10
                }
            }).ConfigureAwait(true);

            response.Data.ForEach(i => Tests.Add(new TestModel
            {
                Id = i.Id,
                Name = i.Name
            }));
        }

        public ObservableCollection<TestModel> Tests { get; set; }
        public TestModel CurrentTest { get; set; }

        public ICommand Logout
        {
            get { return new DelegateCommand(() => { AppContext.SetPage(new LoginPage()); }); }
        }

        public ICommand TakeTestClick
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    if (CurrentTest != null)
                    {
                        var box = MessageBox.Show("Вы уверены что хотите начать тестирование?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (box == MessageBoxResult.Yes)
                        {
                            AppContext.SetPage(new QuestionPage
                            {
                                DataContext = new QuestionPageViewModel(CurrentTest)
                            });
                        }
                    }
                });
            }
        }
    }
}