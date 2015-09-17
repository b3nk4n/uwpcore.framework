using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// Service interface to load and save files from the web.
    /// </summary>
    public interface IWebDownloadService
    {
        /// <summary>
        /// Downloads a file from the specified address and returns the file.
        /// </summary>
        /// <param name="fileUri">The URI of the file.</param>
        /// <param name="fileName">The file name to save the file as.</param>
        /// <param name="option">
        /// A value that indicates what to do
        /// if the filename already exists in the current folder.
        /// </param>
        /// <remarks>
        /// If no file name is given - the method will try to find
        /// the suggested file name in the HTTP response
        /// based on the Content-Disposition HTTP header.
        /// </remarks>
        /// <returns>Returns the downloaded file.</returns>
        Task<IStorageFile> DownloadAsync(Uri fileUri, string fileName = null, NameCollisionOption option = NameCollisionOption.GenerateUniqueName);
    }
}
