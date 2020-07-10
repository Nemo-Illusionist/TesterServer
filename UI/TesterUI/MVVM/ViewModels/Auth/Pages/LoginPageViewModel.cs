using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using TesterUI.MVVM.Models;
using TesterUI.MVVM.VIews.Main.Pages;

namespace TesterUI.MVVM.ViewModels.Auth.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
        public Context AppContext { get; set; }
        
        public LoginPageViewModel()
        {
            Init();
        }

        private void Init()
        {
            AppContext = App.Context;
            User = new UserModel();
        }

        public UserModel User { get; set; }
        
        public ICommand LoginClick
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var response = await AppContext.AuthApi.Login(User).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        AppContext.SetToken(response.Content.Token);   
                        AppContext.SetPage(new ProfilePage());
                    }
                    else
                    {
                        MessageBox.Show(response.Error.Content);
                    }
                });
            }
        }
        
        public ICommand ChangePassword
        {
            get
            {
                return new DelegateCommand<PasswordBox>((passwordBox) => { User.Password = passwordBox.Password; });
            }
        }
    }
}