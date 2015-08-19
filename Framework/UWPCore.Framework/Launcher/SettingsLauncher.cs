using System;
using System.Threading.Tasks;

namespace UWPCore.Framework.Launcher
{
    /// <summary>
    /// Launcher class for system settings.
    /// </summary>
    public static class SettingsLauncher
    {
        /// <summary>
        /// Launches the privacy acounts settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchPrivacyAccounts()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-accounts"));
        }

        /// <summary>
        /// Lauchnes the airplane mode settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchAirplaneModeAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-airplanemode:"));
        }

        /// <summary>
        /// Lauchnes the bluetooth settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchBluetoothAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
        }

        /// <summary>
        /// Lauchnes the cellular settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchCellularAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-cellular:"));
        }

        /// <summary>
        /// Lauchnes the email accounts settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchEmailAccountsAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-emailandaccounts:"));
        }

        /// <summary>
        /// Lauchnes the location settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchLocationAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
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
        /// Lauches the battery saver settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchBatterySaverAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-power:"));
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
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-screenrotation:"));
        }

        /// <summary>
        /// Lauches the wifi settings.
        /// </summary>
        /// <returns>Returns true if successful, else false.</returns>
        public static async Task<bool> LaunchWifiAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:"));
        }
    }
}
