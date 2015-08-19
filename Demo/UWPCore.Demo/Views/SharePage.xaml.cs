using System;
using UWPCore.Framework.Share;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharePage : Page
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
