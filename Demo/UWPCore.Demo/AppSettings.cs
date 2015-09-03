using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UWPCore.Framework.Storage;

namespace UWPCore.Demo
{
    /// <summary>
    /// Static class to access the apps settings.
    /// </summary>
    public static class AppSettings
    {
        public static StoredObjectBase<bool> SettingsSampleBoolean = new LocalObject<bool>("sampleBoolean", false);

        public static StoredObjectBase<string> SettingsSampleEnum = new LocalObject<string>("sampleEnum", SettingsEnum.Settings3.ToString());
    }

    public enum SettingsEnum
    {
        [Display(Name = "Settings One")]
        Settings1,
        [Display(Name = "Settings Two")]
        Settings2,
        [Display(Name = "Settings Three")]
        [DefaultValue(true)]
        Settings3,
        [Display(Name = "Settings Four")]
        Settings4,
        [Display(Name = "Settings Five")]
        Settings5
    }
}
