using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System.UserProfile;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service class to access user personalization settings.
    /// </summary>
    public class PersonalizationService : IPersonalizationService
    {
        public bool IsSupported
        {
            get
            {
                return UserProfilePersonalizationSettings.IsSupported();
            }
        }

        public async Task<bool> SetLockScreenAsync(IStorageFile file)
        {
            if (!IsSupported)
                return false;

            return await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(file as StorageFile);
        }

        public async Task<bool> SetWallpaperAsync(IStorageFile file)
        {
            if (!IsSupported)
                return false;

            return await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(file as StorageFile);
        }
    }
}
