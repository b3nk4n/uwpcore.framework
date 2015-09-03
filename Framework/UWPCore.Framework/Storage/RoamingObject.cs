using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// Encapsulates a key/value pair stored in a roaming data container.
    /// </summary>
    /// <typeparam name="T">The type to store</typeparam>
    public class RoamingObject<T> : StoredObjectBase<T>
    {
        /// <summary>
        /// Creates a new RoamingObject instance.
        /// </summary>
        /// <remarks>Make sure not to reach the quota.</remarks>
        /// <param name="key">The stored ojects key.</param>
        /// <param name="defaultValue">The default value.</param>
        public RoamingObject(string key, T defaultValue)
            : base(ApplicationData.Current.RoamingSettings, key, defaultValue)
        {
        }
    }
}
