using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// The storage service class for shared local app data.
    /// </summary>
    public sealed class SharedLocalStorageService : StorageServiceBase
    {
        /// <summary>
        /// Creates a TemporaryStorageService instance.
        /// </summary>
        public SharedLocalStorageService()
            : base(ApplicationData.Current.TemporaryFolder)
        {
        }
    }
}
