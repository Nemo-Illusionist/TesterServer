using System.Windows.Input;
using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using TesterUI.MVVM.ViewModels;

namespace TesterUI.MVVM.Models
{
    public class DialogModel : BaseViewModel
    {
        public DialogModel()
        {
        }

        public DialogModel(string header, string content, DialogType type)
        {
            Header = header;
            Content = content;
            Type = type;
        }

        public string Header { get; set; }
        public string Content { get; set; }
        public DialogType Type { get; set; }

        public ResultDialogType ResultType { set; get; }

        public ICommand CloseOk
        {
            get
            {
                return new DelegateCommand<DialogHost>(async (dialogHost) =>
                {
                    ResultType = ResultDialogType.Ok;
                    dialogHost.IsOpen = false;
                });
            }
        }

        public ICommand CloseYes
        {
            get
            {
                return new DelegateCommand<DialogHost>(dialogHost =>
                {
                    ResultType = ResultDialogType.Yes;
                    dialogHost.IsOpen = false;
                });
            }
        }

        public ICommand CloseNot
        {
            get
            {
                return new DelegateCommand<DialogHost>(dialogHost =>
                {
                    ResultType = ResultDialogType.Not;
                    dialogHost.IsOpen = false;
                });
            }
        }
    }

    public enum DialogType
    {
        Ok,
        YesNot
    }

    public enum ResultDialogType
    {
        Ok,
        Yes,
        Not
    }
}