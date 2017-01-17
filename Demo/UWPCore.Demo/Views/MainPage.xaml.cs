using System.Threading.Tasks;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Support;
using UWPCore.Framework.UI;
using Windows.ApplicationModel;
using Windows.ApplicationModel.VoiceCommands;
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

        public override Task OnSuspendingAsync(SuspendingEventArgs e)
        {
            Logger.WriteLine("Suspending MainPage.");
            return base.OnSuspendingAsync(e);
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

        private void KeyboardClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(KeyboardPage));
        }

        private void SettingsWithViewModelClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithViewModel));
        }

        private void SettingsWithViewModelAndStringSourceClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithViewModelAndStringSource));
        }

        private void SettingsWithViewModelAndComboBoxStyleClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithComboBoxStyleAndViewModel));
        }

        private void SettingsWithViewModelAndComboBoxStyle2Clicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithComboBoxStyle2AndViewModel));
        }

        private void SettingsWithViewModelAndToggleButtonStyleClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithToggleButtonStyleAndViewModel));
        }

        private void SettingsHitTestViewModelClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithHitTestViewModel));
        }

        private void SettingsWithViewModelAndComboBoxStyleDefaultCopyClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsPageWithDefaultComboBoxStyleCopyAndViewModel));
        }

        private void SerializationTestClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SerializationPerformanceTestPage));
        }

        private async void CheckVoiceCommandInstallClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // takes about 20 seconds!
            var x = VoiceCommandDefinitionManager.InstalledCommandDefinitions.Count;

            await _dialogService.ShowAsync(x.ToString(), "Installed Voice Command count");
        }
    }
}
