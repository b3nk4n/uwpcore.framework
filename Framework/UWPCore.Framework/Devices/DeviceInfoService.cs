using Windows.Security.ExchangeActiveSyncProvisioning;

namespace UWPCore.Framework.Devices
{
    public class DeviceInfoService : IDeviceInfoService
    {
        /// <summary>
        /// The Windows Desktop/Tablet device type.
        /// </summary>
        public const string DEVICE_TYPE_WINDOWS = "WINDOWS";

        /// <summary>
        /// The Windows Mobile device type.
        /// </summary>
        public const string DEVICE_TYPE_MOBILE = "WindowsPhone";

        /// <summary>
        /// The device information client.
        /// </summary>
        private EasClientDeviceInformation _deviceInformation;

        /// <summary>
        /// Creates a DeviceInfoService instance.
        /// </summary>
        public DeviceInfoService()
        {
            _deviceInformation = new EasClientDeviceInformation();
        }

        public bool IsSupported
        {
            get
            {
                return true;
            }
        }

        public bool IsPhone
        {
            get
            {
                string system = _deviceInformation.OperatingSystem;
                return system == DEVICE_TYPE_MOBILE;
            }
        }

        public bool IsWindows
        {
            get
            {
                string system = _deviceInformation.OperatingSystem;
                return system == DEVICE_TYPE_WINDOWS;
            }
        }
    }
}
