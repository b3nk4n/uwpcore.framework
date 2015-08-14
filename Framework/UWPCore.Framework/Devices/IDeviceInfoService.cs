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
    }
}
