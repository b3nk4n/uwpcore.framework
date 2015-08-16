using UWPCore.Framework.Notifications;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// Demo page for all notification services.
    /// </summary>
    public sealed partial class NotificationsPage : Page
    {
        private IToastService _toastService;

        public NotificationsPage()
        {
            InitializeComponent();

            _toastService = new ToastService();
        }

        private void NotifyClicked(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text;
            var content1 = Content1TextBox.Text;
            var content2 = Content2TextBox.Text;
            var imageUri = ImageUriTextBox.Text;

            var selectedTileTemplate = (ToastTemplateComboBox.SelectedItem as ComboBoxItem).Content as string;
            ToastNotification toast = null;
            switch (selectedTileTemplate)
            {
                case "ToastText1":
                    toast = _toastService.CreateToastText01(content1);
                    break;
                case "ToastText2":
                    toast = _toastService.CreateToastText02(title, content1);
                    break;
                case "ToastText3":
                    toast = _toastService.CreateToastText03(title, content1);
                    break;
                case "ToastText4":
                    toast = _toastService.CreateToastText04(title, content1, content2);
                    break;
                case "ToastImageAndText1":
                    toast = _toastService.CreateToastImageAndText01(imageUri, content1);
                    break;
                case "ToastImageAndText2":
                    toast = _toastService.CreateToastImageAndText02(imageUri, title,  content1);
                    break;
                case "ToastImageAndText3":
                    toast = _toastService.CreateToastImageAndText03(imageUri, title, content1);
                    break;
                case "ToastImageAndText4":
                    toast = _toastService.CreateToastImageAndText04(imageUri, title, content1, content2);
                    break;
            }

            _toastService.Show(toast);
        }

        private void ClearHistoryClicked(object sender, RoutedEventArgs e)
        {
            _toastService.ClearHistory();
        }

        private void RemoveByTagClicked(object sender, RoutedEventArgs e)
        {
            var tag = TagTextBox.Text;
            _toastService.RemoveFromHistory(tag);
        }

        private void RemoveByGroupClicked(object sender, RoutedEventArgs e)
        {
            var group = GroupTextBox.Text;
            _toastService.RemoveGroupeFromHistory(group);
        }
    }
}
