using Windows.Storage;

namespace UWPCore.Framework.Share
{
    /// <summary>
    /// Interface for email service operations.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Shows the default application to send an email.
        /// </summary>
        /// <param name="recipients">The recipient emails.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <param name="attachmentFile">The attachement file.</param>
        void Show(string[] recipients, string subject, string body, StorageFile attachmentFile = null);
    }
}
