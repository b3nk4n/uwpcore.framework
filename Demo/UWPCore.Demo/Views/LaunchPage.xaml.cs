using System;
using UWPCore.Framework.Launcher;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LaunchPage : Page
    {
        public LaunchPage()
        {
            InitializeComponent();
        }

        private async void LaunchSettingClicked(object sender, RoutedEventArgs e)
        {
            var settingCommand = (SettingCommandsComboBox.SelectedItem as ComboBoxItem).Content as string;

            switch(settingCommand)
            {
                case "LaunchPrivacyAccounts":
                    await SettingsLauncher.LaunchPrivacyAccountsAsync();
                    break;

                case "LaunchAirplaneMode":
                    await SettingsLauncher.LaunchAirplaneModeAsync();
                    break;

                case "LaunchBluetooth":
                    await SettingsLauncher.LaunchBluetoothAsync();
                    break;

                case "LaunchCellular":
                    await SettingsLauncher.LaunchCellularAsync();
                    break;

                case "LaunchEmailAccounts":
                    await SettingsLauncher.LaunchEmailAccountsAsync();
                    break;

                case "LaunchLocation":
                    await SettingsLauncher.LaunchLocationAsync();
                    break;

                case "LaunchLockScreen":
                    await SettingsLauncher.LaunchLockScreenAsync();
                    break;

                case "LaunchBatterySaver":
                    await SettingsLauncher.LaunchBatterySaverAsync();
                    break;

                case "LaunchScreenRotation":
                    await SettingsLauncher.LaunchScreenRotationAsync();
                    break;

                case "LaunchWifi":
                    await SettingsLauncher.LaunchWifiAsync();
                    break;
            }
        }

        private async void LaunchStoreClicked(object sender, RoutedEventArgs e)
        {
            var settingCommand = (StoreCommandsComboBox.SelectedItem as ComboBoxItem).Content as string;
            var parameter = StoreParameterTextBox.Text;

            switch (settingCommand)
            {
                case "LaunchHome":
                    await StoreLauncher.LaunchHomeAsync();
                    break;

                case "LaunchTopLevelCategory":
                    var category = StoreTopLevelCategory.Music;
                    Enum.TryParse(parameter, out category);

                    await StoreLauncher.LaunchTopLevelCategoryAsync(category);
                    break;

                case "LaunchApp":
                    await StoreLauncher.LaunchAppAsync(parameter);
                    break;

                case "LaunchReview":
                    await StoreLauncher.LaunchReviewAsync(parameter);
                    break;

                case "LaunchSearchAppsByFileExtension":
                    await StoreLauncher.LaunchSearchAppsByFileExtensionAsync(parameter);
                    break;

                case "LaunchSearchAppsByProtocol":
                    await StoreLauncher.LaunchSearchAppsByProtocolAsync(parameter);
                    break;

                case "LaunchSearchAppsByTags":
                    var splittedTags = parameter.Split(new []{ ','}, StringSplitOptions.RemoveEmptyEntries);
                    await StoreLauncher.LaunchSearchAppsByTagsAsync(splittedTags);
                    break;

                case "LaunchSearch":
                    await StoreLauncher.LaunchSearchAsync(parameter);
                    break;

                case "LaunchCategory":
                    var cat = StoreProductCategory.HealthFitness;
                    Enum.TryParse(parameter, out cat);

                    await StoreLauncher.LaunchCategoryAsync(cat);
                    break;

                case "LaunchSearchAppsByPublisher":
                    await StoreLauncher.LaunchSearchAppsByPublisherAsync(parameter);
                    break;

                case "LaunchDownloadAndUpdates":
                    await StoreLauncher.LaunchDownloadAndUpdatesAsync();
                    break;

                case "LaunchSettings":
                    await StoreLauncher.LaunchSettingsAsync();
                    break;
            }
        }
    }
}