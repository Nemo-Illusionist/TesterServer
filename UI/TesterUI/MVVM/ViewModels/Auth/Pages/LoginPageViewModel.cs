using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using TesterUI.Helpers.WpfExtensions;
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
                    if (string.IsNullOrWhiteSpace(User.Login) || string.IsNullOrWhiteSpace(User.Password))
                    {
                        await AppContext.MainDialog.ShowDialog(new DialogModel("Внимание", "Поля логин и пароль обязательные",
                            DialogType.Ok)).ConfigureAwait(true);
                        return;
                    }

                    var response = await AppContext.AuthApi.Login(User).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        AppContext.SetToken(response.Content.Token);
                        AppContext.Login = User.Login;
                        AppContext.SetPage(new ProfilePage());
                    }
                    else
                    {
                        await AppContext.MainDialog.ShowDialog(new DialogModel("Внимание", "Логин или пароль указаны не верно!",
                            DialogType.Ok)).ConfigureAwait(true);
                    }
                });
            }
        }

        public ICommand ChangePassword
        {
            get { return new DelegateCommand<PasswordBox>((passwordBox) => { User.Password = passwordBox.Password; }); }
        }
    }
}