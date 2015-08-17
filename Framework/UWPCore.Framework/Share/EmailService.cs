using System;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Share
{
    /// <summary>
    /// The email service class that provides basic email functionality.
    /// </summary>
    public class EmailService : IEmailService
    {
        public async void Show(string[] recipientEmails, string emailBody, StorageFile attachmentFile = null)
        {
            var emailMessage = new EmailMessage();
            emailMessage.Body = emailBody;

            // email recipients
            foreach (var recipientEmail in recipientEmails)
            {
                var email = new EmailRecipient(recipientEmail);
                emailMessage.To.Add(email);
            }

            // attachement file
            if (attachmentFile != null)
            {
                var stream = RandomAccessStreamReference.CreateFromFile(attachmentFile);
                var attachment = new EmailAttachment(attachmentFile.Name, stream);
                emailMessage.Attachments.Add(attachment);
            }

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
}
