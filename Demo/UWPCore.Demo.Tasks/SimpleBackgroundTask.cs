using System;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.Notifications;
using Windows.ApplicationModel.Background;

namespace UWPCore.Demo.Tasks
{
    /// <summary>
    /// A simple background task to test the functionality.
    /// </summary>
    public sealed class SimpleBackgroundTask : IBackgroundTask
    {
        private IToastService _toastService;

        private Localizer _localizer = new Localizer("UWPCore.Demo.Common");

        public SimpleBackgroundTask()
        {
            _toastService = new ToastService();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            // push a toast notification
            var toast = _toastService.Factory.CreateToastText02(_localizer.Get("Welcome.Text"), DateTime.Now.ToString());
            _toastService.Show(toast);

            // just a short delay to simulate some work
            await Task.Delay(100);

            deferral.Complete();
        }
    }
}
