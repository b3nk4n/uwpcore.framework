using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Service class for toast notifications.
    /// </summary>
    /// <remarks>
    /// After the creation of a toast, the Tag, Group, ExpirationTime or SuppressPopus can be configured.
    /// </remarks>
    public class ToastService : IToastService
    {
        /// <summary>
        /// The toast factory.
        /// </summary>
        private IToastFactory _toastFactory;

        /// <summary>
        /// Creates a ToastService instance.
        /// </summary>
        public ToastService()
        {
            _toastFactory = new ToastFactory();
        }

        public void Show(ToastNotification toast)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
        }

        public void ClearHistory()
        {
            ToastNotificationManager.History.Clear();
        }

        public void RemoveGroupeFromHistory(string group)
        {
            ToastNotificationManager.History.RemoveGroup(group);
        }

        public void RemoveFromHistory(string tag)
        {
            ToastNotificationManager.History.Remove(tag);
        }

        public void RemoveFromHistory(string tag, string group)
        {
            ToastNotificationManager.History.Remove(tag, group);
        }

        public IToastFactory Factory
        {
            get
            {
                return _toastFactory;
            }
        }
    }
}
