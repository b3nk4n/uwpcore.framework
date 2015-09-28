using UWPCore.Framework.Controls;
using UWPCore.Framework.Devices;
using UWPCore.Framework.Storage;
using UWPCore.Framework.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// Demo page for testing device dependent features.
    /// </summary>
    public sealed partial class DeviceFeaturesPage : UniversalPage
    {
        private IVibrateService _vibrateService;

        private IDeviceInfoService _deviceInfoService;

        private IStatusBarService _statusBarService;

        private IStorageService _storageService;

        private ILockScreenService _lockScreenService;

        private IDialogService _dialogService;

        public DeviceFeaturesPage()
        {
            InitializeComponent();

            _vibrateService = new VibrateService();
            _deviceInfoService = new DeviceInfoService();
            _statusBarService = new StatusBarService();
            _storageService = new LocalStorageService();
            _lockScreenService = new LockScreenService();
            _dialogService = new DialogService();
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
            int duration = 100;
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

        private async void SetLockScreenClicked(object sender, RoutedEventArgs e)
        {
            var lockScreenImagePath = LockScreenImageTextBox.Text;

            var file = await _storageService.GetFileFromApplicationAsync(lockScreenImagePath);

            if (file != null)
                await _lockScreenService.SetImageAsync(file);
        }

        private async void GetLockScreenUriClicked(object sender, RoutedEventArgs e)
        {
            var uri = _lockScreenService.GetImageUri();

            if (uri != null)
            {
                await _dialogService.ShowAsync(uri.AbsolutePath, "Info");
            }
        }

        private void GetLockscreenImageClicked(object sender, RoutedEventArgs e)
        {
            var imageStream = _lockScreenService.GetImageStream();
            
            if (imageStream != null)
            {
                var imageSource = new BitmapImage();
                imageSource.SetSource(imageStream);
                LockScreenImage.Source = imageSource;
            }
        }
    }
}
