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
    public sealed class PhoneVibrateService : IVibrateService
    {
        /// <summary>
        /// The vibration device.
        /// </summary>
        private VibrationDevice _virationDevice;

        /// <summary>
        /// Creates a <see cref="UniversalKit.Framework.Phone.Devices.PhoneVibrateService"/> instance.
        /// </summary>
        public PhoneVibrateService()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice"))
            {
                _virationDevice = VibrationDevice.GetDefault();
            }
        }

        public void Vibrate(double seconds)
        {
            if (_virationDevice != null)
                _virationDevice.Vibrate(TimeSpan.FromSeconds(seconds));
        }

        public void Stop()
        {
            if (_virationDevice != null)
                _virationDevice.Cancel();
        }
    }

}
