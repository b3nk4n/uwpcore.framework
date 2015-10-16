using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;

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
        /// Checks if an URI/app can be launched.
        /// </summary>
        /// <param name="uri">The URI of the app to check.</param>
        /// <returns>Returns True if possible, else False.</returns>
        public static async Task<bool> QueryUriSupportAsync(Uri uri)
        {
            var result = await Windows.System.Launcher.QueryUriSupportAsync(uri, LaunchQuerySupportType.UriForResults);
            return result == LaunchQuerySupportStatus.Available;
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
        /// Checks if there is an app to launch a given file.
        /// </summary>
        /// <param name="uri">The file to check.</param>
        /// <returns>Returns True if possible, else False.</returns>
        public static async Task<bool> QueryFileSupportAsync(IStorageFile file)
        {
            var result = await Windows.System.Launcher.QueryFileSupportAsync(file as StorageFile);
            return result == LaunchQuerySupportStatus.Available;
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
