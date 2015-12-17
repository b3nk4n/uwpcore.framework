using UWPCore.Framework.Controls;
using UWPCore.Framework.Support;
using UWPCore.Framework.UI;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : UniversalPage
    {
        private IStartupActionService _startupActions;
        private IDialogService _dialogService;

        public MainPage()
        {
            InitializeComponent();
            _startupActions = Injector.Get<IStartupActionService>();
            _dialogService = Injector.Get<IDialogService>();

            _startupActions.Register(5, ActionExecutionRule.Equals, async () =>
            {
                await _dialogService.ShowAsync("This is your " + _startupActions.Count + "th app launch." , "Info");
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _startupActions.OnNavigatedTo(e.NavigationMode);
        }

        private void SpeechClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SpeechPage));
        }

        private void SettingsClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPage));
        }

        private void AccountsClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(AccountsPage));
        }

        private void LauncherClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(LaunchPage));
        }

        private void ShareClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SharePage));
        }

        private void DeviceFeaturesClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(DeviceFeaturesPage));
        }

        private void AudioClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(AudioPage));
        }
    }
}
