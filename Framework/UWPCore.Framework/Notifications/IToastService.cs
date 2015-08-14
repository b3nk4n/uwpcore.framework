using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for toast notification service.
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastText1(string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastText02(string title, string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastText03(string title, string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="content1">The first content.</param>
        /// <param name="content2">The second content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastText04(string title, string content1, string content2, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="image">The image source.</param>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastImageAndText01(string image, string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="image">The image source.</param>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastImageAndText02(string image, string title, string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="image">The image source.</param>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastImageAndText03(string image, string title, string content, string arg = null);

        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="image">The image source.</param>
        /// <param name="content1">The first content.</param>
        /// <param name="content2">The second content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastImageAndText04(string image, string title, string content1, string content2, string arg = null);

        /// <summary>
        /// Shows the toast notification.
        /// </summary>
        /// <param name="toast">The notification to show.</param>
        void Show(ToastNotification toast);
    }
}
