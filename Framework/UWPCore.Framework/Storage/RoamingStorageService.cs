using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// The storage service class for raoming app data.
    /// </summary>
    public sealed class RoamingStorageService : StorageServiceBase
    {
        /// <summary>
        /// Creates a RoamingStorageService instance.
        /// </summary>
        public RoamingStorageService()
            : base(ApplicationData.Current.RoamingFolder)
        {
        }
    }
}
