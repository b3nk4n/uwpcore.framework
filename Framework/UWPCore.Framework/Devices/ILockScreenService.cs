using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service interface to access lock screen functions.
    /// </summary>
    public interface ILockScreenService : IDeviceService
    {
        /// <summary>
        /// Sets the lockscreen image.
        /// </summary>
        /// <param name="file">The image file.</param>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> SetImageAsync(IStorageFile file);

        /// <summary>
        /// Sets the lockscreen image.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> SetImageAsync(IRandomAccessStream stream);

        /// <summary>
        /// Gets the current lock screen image as a data stream.
        /// </summary>
        /// <remarks>
        /// This method can be called only by apps that have set the "Picture Library Access" capability in the package manifest
        /// or by the app that set this image on the lock screen.
        /// </remarks>
        /// <returns>The stream that contains the lock screen image data or NULL in case of an error.</returns>
        IRandomAccessStream GetImageStream();

        /// <summary>
        /// Gets the current lock screen image, but retrieves only files, so works only when iamge was not set as a stream.
        /// </summary>
        /// <returns>The image URI of the image or NULL in case of an error.</returns>
        Uri GetImageUri();
    }
}
