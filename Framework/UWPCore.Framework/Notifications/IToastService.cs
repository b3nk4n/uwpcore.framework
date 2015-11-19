using System;
using System.Collections.Generic;
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
        /// Shows the toast notification delayed.
        /// </summary>
        /// <param name="toast">The notification to show.</param>
        /// <param name="when">The time when the toast notification should be scheduled.</param>
        void Show(ToastNotification toast, DateTimeOffset when);

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
        /// Gets the notifications from the action center.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <returns>Returns the notifications that match this criteria.</returns>
        IEnumerable<ToastNotification> GetByTagFromHistory(string tag);

        /// <summary>
        /// Gets the notifications from the action center.
        /// </summary>
        /// <param name="group">The group name.</param>
        /// <returns>Returns the notifications that match this criteria.</returns>
        IEnumerable<ToastNotification> GetByGroupFromHistory(string group);

        /// <summary>
        /// Gets all notifications from the action center.
        /// </summary>
        IReadOnlyList<ToastNotification> History { get; }

        /// <summary>
        /// Gets the toast factory.
        /// </summary>
        IToastFactory Factory { get; }

        /// <summary>
        /// Gets the adaptive factory
        /// </summary>
        IAdaptiveToastFactory AdaptiveFactory { get; }
    }
}
