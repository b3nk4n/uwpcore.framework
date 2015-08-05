
namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// The network information interface to retrive information about the network
    /// and the devices connectiviy to the internet.
    /// </summary>
    public interface INetworkInfoService
    {
        /// <summary>
        /// Gets whether the user has internet connection.
        /// </summary>
        bool HasInternet { get; }

        /// <summary>
        /// Gets whether the user has Wifi connection.
        /// </summary>
        bool HasWifi { get; }

        /// <summary>
        /// Gets whether the user has no data connection.
        /// </summary>
        bool HasNoConnection { get; }
    }
}
