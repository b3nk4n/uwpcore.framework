using Windows.Networking.Connectivity;

namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// The network information class that provides information about the devies connectivity.
    /// </summary>
    public class NetworkInfoService : INetworkInfoService
    {
        /// <summary>
        /// Some other type of network interface.
        /// </summary>
        public const uint IANA_OTHER = 1;

        /// <summary>
        /// An Ethernet network interface.
        /// </summary>
        public const uint IANA_ETHERNET = 6;

        /// <summary>
        /// An IEEE 802.11 wireless network interface.
        /// </summary>
        public const uint IANA_WLAN = 71;

        public bool HasInternet
        {
            get
            {
                var connections = NetworkInformation.GetInternetConnectionProfile();
                return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            }
        }

        public bool HasWifi
        {
            get
            {
                var connections = NetworkInformation.GetInternetConnectionProfile();
                return connections.NetworkAdapter.IanaInterfaceType == IANA_WLAN;
            }
        }

        public bool HasNoConnection
        {
            get
            {
                var connections = NetworkInformation.GetInternetConnectionProfile();
                return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.None;
            }
        }
    }
}
