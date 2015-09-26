using UWPCore.Framework.Common;
using UWPCore.Framework.Logging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationPage1 : Page
    {
        public NavigationPage1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Logger.WriteLine("PAGE - OnNavigatedTo");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Logger.WriteLine("PAGE - OnNavigatingFrom");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Logger.WriteLine("PAGE - OnNavigatedFrom");
        }

        private void GoToSubpageClicked(object sender, RoutedEventArgs e)
        {
            UniversalApp.Current.NavigationService.Navigate(typeof(NavigationPage2));
        }
    }
}
