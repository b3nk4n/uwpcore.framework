using System.Threading.Tasks;
using Windows.Storage;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service interface to access user personalization settings.
    /// </summary>
    public interface IPersonalizationService
    {
        /// <summary>
        /// Gets whether this service is supported on the current device.
        /// </summary>
        bool IsSupported { get; }

        /// <summary>
        /// Sets the lock screen image.
        /// </summary>
        /// <param name="file">The image file.</param>
        /// <returns>True for success, else False.</returns>
        Task<bool> SetLockScreenAsync(IStorageFile file);

        /// <summary>
        /// Sets the wallpaper image.
        /// </summary>
        /// <param name="file">The image file.</param>
        /// <returns>True for success, else False.</returns>
        Task<bool> SetWallpaperAsync(IStorageFile file);
    }
}
