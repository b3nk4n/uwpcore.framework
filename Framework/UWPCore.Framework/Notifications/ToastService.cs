using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// The adaptive toast factory.
        /// </summary>
        private IAdaptiveToastFactory _adaptiveToastFactory;

        /// <summary>
        /// Creates a ToastService instance.
        /// </summary>
        [Inject]
        public ToastService(IToastFactory toastFactory, IAdaptiveToastFactory adaptiveToastFactory)
        {
            _toastFactory = toastFactory;
            _adaptiveToastFactory = adaptiveToastFactory;
        }

        public void Show(ToastNotification toast)
        {
            try
            {
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(toast);
            }
            catch (Exception)
            {
                // Paranoia catch, because we got a couple of crashes from this code region:
                // See: https://social.msdn.microsoft.com/Forums/windowsapps/de-DE/ee869674-84de-432a-a61a-6ed9c2bfd6bd/uwptoastnotifiershow-causes-crashes-stowedexceptionsystemexception?forum=wpdevelop
            }
        }

        public void Show(ToastNotification toast, DateTimeOffset when)
        {
            // TODO: check if DateTime offset > DateTimeNow

            var notifier = ToastNotificationManager.CreateToastNotifier();
            var scheduled = new ScheduledToastNotification(toast.Content, when);
            scheduled.Group = toast.Group;
            scheduled.Tag = toast.Tag;
            scheduled.SuppressPopup = toast.SuppressPopup;
            notifier.AddToSchedule(scheduled);
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

        public IEnumerable<ToastNotification> GetByTagFromHistory(string tag)
        {
            return ToastNotificationManager.History.GetHistory().Where(t => t.Tag == tag);
        }

        public IEnumerable<ToastNotification> GetByGroupFromHistory(string group)
        {
            return ToastNotificationManager.History.GetHistory().Where(t => t.Group == group);
        }

        public IReadOnlyList<ToastNotification> History
        {
            get
            {
                return ToastNotificationManager.History.GetHistory();
            }
        }

        public IToastFactory Factory
        {
            get
            {
                return _toastFactory;
            }
        }

        public IAdaptiveToastFactory AdaptiveFactory
        {
            get
            {
                return _adaptiveToastFactory;
            }
        }
    }
}
