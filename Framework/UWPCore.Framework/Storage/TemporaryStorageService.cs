using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// The storage service class for temporary app data.
    /// </summary>
    public sealed class TemporaryStorageService : StorageServiceBase, ITemporaryStorageService
    {
        /// <summary>
        /// Creates a TemporaryStorageService instance.
        /// </summary>
        public TemporaryStorageService()
            : base(ApplicationData.Current.TemporaryFolder)
        {
        }
    }
}
