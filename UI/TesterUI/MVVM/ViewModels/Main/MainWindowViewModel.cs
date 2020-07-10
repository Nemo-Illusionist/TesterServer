using System.Windows.Input;
using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using TesterUI.MVVM.Models;
using TesterUI.MVVM.VIews.Auth.Pages;

namespace TesterUI.MVVM.ViewModels.Main
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Context AppContext { get; set; }

        public MainWindowViewModel()
        {
            Init();
        }

        private void Init()
        {
            AppContext = App.Context;
            AppContext.SetPage(new LoginPage());
        }

        public ICommand LoadedWindow
        {
            get { return new DelegateCommand<DialogHost>((dialog) => { AppContext.MainDialog = dialog; }); }
        }
    }
}