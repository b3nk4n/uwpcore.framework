using System;
using System.Threading.Tasks;

namespace UWPCore.Framework.Launcher
{
    /// <summary>
    /// Launcher class for system settings.
    /// <seealso cref="https://msdn.microsoft.com/en-us/windows/uwp/launch-resume/launch-settings-app"/>
    /// </summary>
    public static class SettingsLauncher
    {
        /// <summary>
        /// Lauchnes the airplane mode settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchAirplaneModeAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:network-airplanemode"));
        }

        /// <summary>
        /// Lauchnes the bluetooth settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchBluetoothAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:bluetooth"));
        }

        /// <summary>
        /// Lauchnes the cellular settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchCellularAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:network-cellular"));
        }

        /// <summary>
        /// Lauchnes the email accounts settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchEmailAccountsAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:emailandaccounts"));
        }

        /// <summary>
        /// Lauchnes the location settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchLocationAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
        }

        /// <summary>
        /// Lauchnes the lock screen settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchLockScreenAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        /// <summary>
        /// Lauchnes the Notifications & Actions settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchNotificationsAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:screenrotation"));
        }

        /// <summary>
        /// Lauches the battery saver settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchBatterySaverAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:batterysaver"));
        }

        /// <summary>
        /// Launches the privacy acounts settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchPrivacyAccountsAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-accounts"));
        }

        /// <summary>
        /// Launches the privacy webcam settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchPrivacyWebcamAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
        }

        /// <summary>
        /// Lauchnes the screen rotation settings.
        /// </summary>
        /// <remarks>
        /// Requires the GDR3 update.
        /// </remarks>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchScreenRotationAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:screenrotation"));
        }

        /// <summary>
        /// Lauches the wifi settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchWifiAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:network-wifi"));
        }
    }
}
