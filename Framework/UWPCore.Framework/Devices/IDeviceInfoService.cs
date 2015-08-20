namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Interface to provide information about the active device.
    /// </summary>
    public interface IDeviceInfoService : IDeviceService
    {
        /// <summary>
        /// Gets whether the device is a phone or not.
        /// </summary>
        bool IsPhone { get; }

        /// <summary>
        /// Gets whether the device is a desktop/tablet or not.
        /// </summary>
        bool IsWindows { get; }

        /// <summary>
        /// Gets the hardware ID.
        /// </summary>
        string HardwareId { get; }

        /// <summary>
        /// Gets the application ID.
        /// </summary>
        string ApplicationId { get; }
    }
}
