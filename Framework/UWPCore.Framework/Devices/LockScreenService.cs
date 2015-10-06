using System;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service class to access lock screen functions.
    /// </summary>
    /// <remarks>
    /// Might require the "Picture Library Access" capability when <see cref="GetImageStream()"/> is used, but the stream was set by a different app.
    /// </remarks>
    [Obsolete("Use UWPCore.Framework.Devices.PersonalizationService instead.")]
    public class LockScreenService : ILockScreenService
    {
        public async Task<bool> SetImageAsync(IStorageFile file)
        {
            if (ApiInformation.IsApiContractPresent("Windows.System.UserProfile.UserProfileLockScreenContract", 1))
            {
                try
                {
                    await LockScreen.SetImageFileAsync(file);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex, "Could not set the lock screen image file.");
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> SetImageAsync(IRandomAccessStream stream)
        {
            if (ApiInformation.IsApiContractPresent("Windows.System.UserProfile.UserProfileLockScreenContract", 1))
            {
                try
                {
                    await LockScreen.SetImageStreamAsync(stream);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex, "Could not set the lock screen image stream.");
                    return false;
                }
            }

            return false;
        }

        public IRandomAccessStream GetImageStream()
        {
            if (ApiInformation.IsApiContractPresent("Windows.System.UserProfile.UserProfileLockScreenContract", 1))
            {
                try
                {
                    return LockScreen.GetImageStream();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex, "Could not acces the lock screen image stream.");
                    return null;
                }
            }

            return null;
        }

        public Uri GetImageUri()
        {
            if (ApiInformation.IsApiContractPresent("Windows.System.UserProfile.UserProfileLockScreenContract", 1))
            {
                try
                {
                    return LockScreen.OriginalImageFile;
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex, "Could not get the lock screen image file URI. Keep in mind this function is not allowed when using image streams.");
                    return null;
                }
            }

            return null;
        }
    }
}
