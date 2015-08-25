using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for toast notification service.
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Shows the toast notification.
        /// </summary>
        /// <param name="toast">The notification to show.</param>
        void Show(ToastNotification toast);

        /// <summary>
        /// Clears the action center history.
        /// </summary>
        void ClearHistory();
        
        /// <summary>
        /// Removes a toast group from the action center history.
        /// </summary>
        /// <param name="group">The toast group name.</param>
        void RemoveGroupeFromHistory(string group);
        
        /// <summary>
        /// Removes a toast notification from the action center history.
        /// </summary>
        /// <param name="tag">The toast tag name.</param>
        void RemoveFromHistory(string tag);

        /// <summary>
        /// Removes a toast notification from the action center history.
        /// </summary>
        /// <param name="tag">The toast tag name.</param>
        /// <param name="group">The toast group name.</param>
        void RemoveFromHistory(string tag, string group);

        /// <summary>
        /// Gets the toast factory.
        /// </summary>
        IToastFactory Factory { get; }
    }
}
