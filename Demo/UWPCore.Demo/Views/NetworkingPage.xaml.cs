﻿using UWPCore.Framework.Devices;
using UWPCore.Framework.Networking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// The demo page for networking stuff.
    /// </summary>
    public sealed partial class NetworkingPage : Page
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