using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using TesterUI.MVVM.Models;

namespace TesterUI.Helpers.WpfExtensions
{
    public static class DialogHostExtension
    {
        public static async Task<ResultDialogType> ShowDialog([NotNull] this DialogHost dialog, [NotNull] DialogModel context)
        {
            if (dialog == null) throw new ArgumentNullException(nameof(dialog));
            dialog.DataContext = context ?? throw new ArgumentNullException(nameof(context));

            dialog.IsOpen = true;

            while (dialog.IsOpen)
            {
                await Task.Delay(1000).ConfigureAwait(true);
            }

            return context.ResultType;
        }
    }
}