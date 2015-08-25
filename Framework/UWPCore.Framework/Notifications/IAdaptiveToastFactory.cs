using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for a factory to create adaptive toast notifications.
    /// </summary>
    public interface IAdaptiveToastFactory
    {
        /// <summary>
        /// Creates an adaptive toast notification.
        /// </summary>
        /// <param name="toast">The toast model to create from.</param>
        /// <returns>Returns a toast notification.</returns>
        ToastNotification Create(AdaptiveToastModel toast);
    }
}
