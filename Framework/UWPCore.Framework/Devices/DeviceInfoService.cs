﻿using System;
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

        public Version WindowsVersion
        {
            get
            {
                return Version.GetCurrent();
            }
        }

        public bool IsAnniversaryUpdateInstalled
        {
            get
            {
                var version = Version.GetCurrent();
                if (version.Major < 10)
                    return false;
                if (version.Major > 10)
                    return true;

                // major 10
                if (version.Minor > 0)
                    return true;

                // major 10, minor 0
                if (version.Build >= 14393)
                    return true;

                return false;
            }
        }

        public bool IsAnniversaryUpdateOrPreviewInstalled
        {
            get
            {
                var version = Version.GetCurrent();
                if (version.Major < 10)
                    return false;
                if (version.Major > 10)
                    return true;

                // major 10
                if (version.Minor > 0)
                    return true;

                // major 10, minor 0
                if (version.Build >= 14000)
                    return true;

                return false;
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

            _hardwareId = BitConverter.ToString(bytes);
            return _hardwareId;
        }
    }

    public class Version
    {
        public ulong Major { get; private set; }
        public ulong Minor { get; private set; }
        public ulong Build { get; private set; }
        public ulong Revision { get; private set; }

        public Version(ulong major, ulong minor, ulong build, ulong revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public static Version GetCurrent()
        {
            string deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong version = ulong.Parse(deviceFamilyVersion);
            ulong major = (version & 0xFFFF000000000000L) >> 48;
            ulong minor = (version & 0x0000FFFF00000000L) >> 32;
            ulong build = (version & 0x00000000FFFF0000L) >> 16;
            ulong revision = (version & 0x000000000000FFFFL);
            return new Version(major, minor, build, revision);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Build}.{Revision}";
        }
    }
}
