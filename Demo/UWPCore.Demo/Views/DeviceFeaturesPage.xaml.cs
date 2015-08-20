using UWPCore.Framework.Devices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// Demo page for testing device dependent features.
    /// </summary>
    public sealed partial class DeviceFeaturesPage : Page
    {
        private IVibrateService _vibrateService;

        private IDeviceInfoService _deviceInfoService;

        public DeviceFeaturesPage()
        {
            InitializeComponent();

            _vibrateService = new VibrateService();
            _deviceInfoService = new DeviceInfoService();
        }

        private void VibrateClicked(object sender, RoutedEventArgs e)
        {
            _vibrateService.Vibrate(500);
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
