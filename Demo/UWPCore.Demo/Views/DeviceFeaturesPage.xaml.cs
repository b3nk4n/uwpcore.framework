using UWPCore.Framework.Devices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// Demo page for testing device dependent features.
    /// </summary>
    public sealed partial class DeviceFeaturesPage : Page
    {
        private IVibrateService _vibrateService;

        private IDeviceInfoService _deviceInfoService;

        private IStatusBarService _statusBarService;

        public DeviceFeaturesPage()
        {
            InitializeComponent();

            _vibrateService = new VibrateService();
            _deviceInfoService = new DeviceInfoService();
            _statusBarService = new StatusBarService();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await _statusBarService.StartProgressAsync("Sample progress...", true);
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            await _statusBarService.StopProgressAsync();
        }

        private void VibrateClicked(object sender, RoutedEventArgs e)
        {
            int duration = 0;
            int.TryParse(VibrationDurationTextBox.Text, out duration);

            _vibrateService.Vibrate(duration);
        }

        private void StopClicked(object sender, RoutedEventArgs e)
        {
            _vibrateService.Stop();
        }

        public bool Status
        {
            get
            {
                return _vibrateService.IsSupported;
            }
        }

        public bool IsPhoneStatus
        {
            get
            {
                return _deviceInfoService.IsPhone;
            }
        }

        public bool IsWindowsStatus
        {
            get
            {
                return _deviceInfoService.IsWindows;
            }
        }

        public string ApplicationId
        {
            get
            {
                return _deviceInfoService.ApplicationId;
            }
        }

        public string HardwareId
        {
            get
            {
                return _deviceInfoService.HardwareId;
            }
        }
    }
}
