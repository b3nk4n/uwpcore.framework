using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Interface for compression services.
    /// </summary>
    public interface ICompressionService
    {
        /// <summary>
        /// Compress folder async to file.
        /// </summary>
        /// <param name="folder">Folder to compress. Only folder content will be zipped.</param>
        /// <param name="file">File to compress.</param>
        /// <returns></returns>
        Task<bool> CompressAsync(IStorageFolder folder, IStorageFile file);

        /// <summary>
        /// Uncompress file into folder.
        /// </summary>
        /// <param name="file">Target file to uncompress.</param>
        /// <param name="folder">Folder data should be uncompressed in.</param>
        /// <returns></returns>
        Task<bool> UncompressAsync(IStorageFile file, IStorageFolder folder);

        /// <summary>
        /// Compresses and create an archive of a folder.
        /// </summary>
        /// <param name="path">Path to folder.</param>
        /// <param name="name">Alternative compressed folder name.</param>
        /// <returns>Returns task to await.</returns>
        [Obsolete]
        Task CompressAsync(string path, string name = null);

        /// <summary>
        /// Uncompresses a given archive to a folder.
        /// </summary>
        /// <param name="path">Path to zip archive.</param>
        /// <param name="name">Alternative uncompressed folder name.</param>
        /// <returns>Returns task to await.</returns>
        [Obsolete]
        Task UncompressAsync(string path, string name = null);
    }
}
