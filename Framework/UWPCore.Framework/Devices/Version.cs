using Windows.System.Profile;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Class representing a OS version.
    /// </summary>
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

        /// <summary>
        /// Gets the currents version of the running OS.
        /// </summary>
        /// <returns>Returns the version numbers.</returns>
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
