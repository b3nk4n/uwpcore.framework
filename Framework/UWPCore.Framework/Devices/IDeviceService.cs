namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Interface for device dependent services.
    /// </summary>
    public interface IDeviceService
    {

        /// <summary>
        /// Gets whether the service is supported on the running device.
        /// </summary>
        bool IsSupported { get; }
    }
}
