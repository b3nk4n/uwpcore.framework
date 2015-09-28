using System;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Share;
using Windows.UI.Xaml;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : UniversalPage
    {
        private IEmailService _emailService;

        public SharePage()
        {
            InitializeComponent();
            _emailService = new EmailService();
        }

        private void SendEmailClicked(object sender, RoutedEventArgs e)
        {
            var recipientsString = RecipientsTextBox.Text;
            var recipients = recipientsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var subject = SubjectTextBox.Text;
            var body = BodyTextBox.Text;

            _emailService.Show(recipients, subject, body);
        }
    }
}
