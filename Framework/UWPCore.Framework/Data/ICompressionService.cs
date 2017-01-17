using System.Threading.Tasks;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Interface for compression services.
    /// </summary>
    public interface ICompressionService
    {
        /// <summary>
        /// Compresses and create an archive of a folder.
        /// </summary>
        /// <param name="path">Path to folder.</param>
        /// <param name="name">Alternative compressed folder name.</param>
        /// <returns>Returns task to await.</returns>
        Task CompressAsync(string path, string name = null);

        /// <summary>
        /// Uncompresses a given archive to a folder.
        /// </summary>
        /// <param name="path">Path to zip archive.</param>
        /// <param name="name">Alternative uncompressed folder name.</param>
        /// <returns>Returns task to await.</returns>
        Task UncompressAsync(string path, string name = null);
    }
}
