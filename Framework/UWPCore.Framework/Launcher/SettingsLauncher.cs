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
        public static async Task LaunchPrivacyAccounts()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-accounts"));
        }
    }
}
