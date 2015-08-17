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
        /// <param name="recipientEmails">The recipient emails.</param>
        /// <param name="emailBody">The email body.</param>
        /// <param name="attachmentFile">The attachement file.</param>
        void Show(string[] recipientEmails, string emailBody, StorageFile attachmentFile = null);
    }
}
