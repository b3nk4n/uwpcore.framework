using System;
using Windows.Foundation.Metadata;
using Windows.Phone.Devices.Notification;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// The implementation of the vibration service.
    /// </summary>
    /// <remarks>
    /// This service is only available on mobile devices.
    /// </remarks>
    public sealed class VibrateService : IVibrateService
    {
        /// <summary>
        /// The vibration device.
        /// </summary>
        private VibrationDevice _virationDevice;

        /// <summary>
        /// Creates a VibrateService instance.
        /// </summary>
        public VibrateService()
        {
            if (IsSupported)
            {
                _virationDevice = VibrationDevice.GetDefault();
            }
        }

        public void Vibrate(int millis)
        {
            if (_virationDevice != null)
                _virationDevice.Vibrate(TimeSpan.FromMilliseconds(millis));
        }

        public void Stop()
        {
            if (_virationDevice != null)
                _virationDevice.Cancel();
        }

        public bool IsSupported
        {
            get
            {
                return ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice");
            }
        }
    }

}
