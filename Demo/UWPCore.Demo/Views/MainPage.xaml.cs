using UWPCore.Framework.Controls;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : UniversalPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SettingsClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPage));
        }
    }
}
