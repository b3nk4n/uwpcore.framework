using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// The service class for dialog popup messages.
    /// </summary>
    public class DialogService : IDialogService
    {
        public bool IsOpen { get; private set; } = false;

        public async Task ShowAsync(string content, string title)
        {
            await ShowAsync(content, title, null, null);
        }

        public async Task<IUICommand> ShowAsync(string content, string title, uint? enterIndex, uint? escIndex, params IUICommand[] commands)
        {
            // wait until a previous dialog is closed
            while (IsOpen)
            {
                await Task.Delay(1000);
            }

            IsOpen = true;
            var dialog = new MessageDialog(content, title);
            if (commands != null && commands.Any())
            {
                if (enterIndex.HasValue)
                    dialog.DefaultCommandIndex = enterIndex.Value;

                if (escIndex.HasValue)
                    dialog.CancelCommandIndex = escIndex.Value;

                foreach (var item in commands)
                {
                    dialog.Commands.Add(item);
                }
            }
            var result = await dialog.ShowAsync();
            IsOpen = false;

            return result;
        }
    }
}
