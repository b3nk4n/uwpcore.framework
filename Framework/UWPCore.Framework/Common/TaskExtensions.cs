using System.Diagnostics;
using System.Threading.Tasks;

namespace UWPCore.Framework.Common
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Supress the await warning and run the method in background,
        /// while the code of the calling method continues.
        /// </summary>
        /// <param name="task">The extended task instance.</param>
        public static void Forget(this Task task)
        {
            task.ContinueWith(
                t => {
                    Debug.WriteLine(t.Exception);
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
