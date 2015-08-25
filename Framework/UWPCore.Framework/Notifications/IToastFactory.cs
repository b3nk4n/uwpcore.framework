using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for a factory to create toast notifications from a template.
    /// </summary>
    /// <remarks>
    /// Further information about the templates unter <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh761494.aspx"/>.
    /// </remarks>
    public interface IToastFactory
    {
        /// <summary>
        /// Creates a toast notification.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="arg">The optional launch argument.</param>
        /// <returns>Returns the toast notification.</returns>
        ToastNotification CreateToastText01(string content, string arg = null);

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
    }
}
