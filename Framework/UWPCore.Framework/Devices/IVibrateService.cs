namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// The service for the vibration controller.
    /// </summary>
    public interface IVibrateService : IDeviceService
    {
        /// <summary>
        /// Invokes the vibration controller for the given time.
        /// </summary>
        /// <param name="millis">The vibration time in milliseconds.</param>
        void Vibrate(int millis);

        /// <summary>
        /// Stops the current vibration.
        /// </summary>
        void Stop();
    }

}
