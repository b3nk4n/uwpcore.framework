using System;
using Windows.ApplicationModel.Store;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

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
        /// The cached hardware ID.
        /// </summary>
        private string _hardwareId;

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

        public string HardwareId
        {
            get
            {
                return GetHardwareId();
            }
        }

        public string ApplicationId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }

        /// <summary>
        /// Gets the hardware identification number.
        /// </summary>
        /// <returns>The hardware identification.</returns>
        private string GetHardwareId()
        {
            if (_hardwareId != null)
                return _hardwareId;

            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);
        }
    }
}
