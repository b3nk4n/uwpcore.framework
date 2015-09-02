using UWPCore.Framework.Storage;

namespace UWPCore.Demo
{
    /// <summary>
    /// Static class to access the apps settings.
    /// </summary>
    public static class AppSettings
    {
        public static StoredObject<bool> SettingsSampleToggleSwitch = new StoredObject<bool>("sampleToggleSwitch", false);
    }
}
