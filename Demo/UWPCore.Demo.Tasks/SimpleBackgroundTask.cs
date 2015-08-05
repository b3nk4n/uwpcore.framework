using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace UWPCore.Demo.Tasks
{
    /// <summary>
    /// A simple background task to test the functionality.
    /// </summary>
    public sealed class SimpleBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            await Task.Delay(100);

            deferral.Complete();
        }
    }
}
