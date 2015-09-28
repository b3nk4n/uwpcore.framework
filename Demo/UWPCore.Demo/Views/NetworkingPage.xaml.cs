using UWPCore.Framework.Controls;
using UWPCore.Framework.Networking;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// The demo page for networking stuff.
    /// </summary>
    public sealed partial class NetworkingPage : UniversalPage
    {
        private INetworkInfoService _networkInfoService;

        public NetworkingPage()
        {
            InitializeComponent();

            _networkInfoService = new NetworkInfoService();
        }

        public bool InternetStatus
        {
            get
            {
                return _networkInfoService.HasInternet;
            }
        }

        public bool WifiStatus
        {
            get
            {
                return _networkInfoService.HasWifi;
            }
        }

        public bool ConnectionStatus
        {
            get
            {
                return !_networkInfoService.HasNoConnection;
            }
        }
    }
}
