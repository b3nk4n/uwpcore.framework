using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// Encapsulates a key/value pair stored in a local data container.
    /// </summary>
    /// <typeparam name="T">The type to store</typeparam>
    public class LocalObject<T> : StoredObjectBase<T>
    {
        /// <summary>
        /// Creates a new LocalObject instance.
        /// </summary>
        /// <param name="key">The stored ojects key.</param>
        /// <param name="defaultValue">The default value.</param>
        public LocalObject(string key, T defaultValue)
            : base(ApplicationData.Current.LocalSettings, key, defaultValue)
        {
        }
    }
}
