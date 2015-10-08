using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// The storage service class for local app data.
    /// </summary>
    public sealed class LocalStorageService : StorageServiceBase, ILocalStorageService
    {
        /// <summary>
        /// Creates a LocalStorageService instance.
        /// </summary>
        public LocalStorageService ()
            : base(ApplicationData.Current.LocalFolder)
        {
        }
    }
}
