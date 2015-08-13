using UWPCore.Framework.Devices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeviceFeaturesPage : Page
    {
        private IVibrateService _vibrateService;

        public DeviceFeaturesPage()
        {
            InitializeComponent();

            _vibrateService = new VibrateService();
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
    }
}
