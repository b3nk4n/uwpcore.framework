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

                /*
                 * In case of en exception:
                 * http://stackoverflow.com/questions/13625304/check-internet-connection-availability-in-windows-8
                 *  class ConnectivityUtil
                    {
                        internal static bool HasInternetConnection()
                        {            
                            var connections = NetworkInformation.GetConnectionProfiles().ToList();
                            connections.Add(NetworkInformation.GetInternetConnectionProfile());

                            foreach (var connection in connections)
                            {
                                if (connection == null)
                                    continue;

                                if (connection.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                                    return true;
                            }

                            return false;
                        }
                    }
                 */
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
