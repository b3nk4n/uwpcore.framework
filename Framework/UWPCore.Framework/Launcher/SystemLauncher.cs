using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPCore.Framework.Launcher
{
    /// <summary>
    /// Launcher class for system functionality.
    /// </summary>
    public static class SystemLauncher
    {
        /// <summary>
        /// Launches the URI.
        /// </summary>
        /// <param name="uri">The URI to launch.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchUriAsync(Uri uri)
        {
            return await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        /// <summary>
        /// Launches the file.
        /// </summary>
        /// <param name="file">The file to launch.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchFileAsync(IStorageFile file)
        {
            return await Windows.System.Launcher.LaunchFileAsync(file);
        }

        /// <summary>
        /// Launches the folder.
        /// </summary>
        /// <param name="folder">The folder to launch.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchFolderAsync(IStorageFolder folder)
        {
            return await Windows.System.Launcher.LaunchFolderAsync(folder);
        }
    }
}
