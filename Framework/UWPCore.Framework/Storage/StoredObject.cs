using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// Encapsulates a key/value pair stored in isolated storage.
    /// </summary>
    /// <typeparam name="T">The type to store</typeparam>
    public class StoredObject<T>
    {
        #region Members

        /// <summary>
        /// The local settings data container.
        /// </summary>
        ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// The current value.
        /// </summary>
        private T _value;

        /// <summary>
        /// The default value.
        /// </summary>
        private T _defaultValue;

        /// <summary>
        /// The objects key.
        /// </summary>
        private string _key;

        /// <summary>
        /// Indicates whether a refresh from isolated storage is required.
        /// </summary>
        /// <remarks>
        /// Value must be initially True that the value will be loaded on start up.
        /// </remarks>
        private bool _needRefresh = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new StoredObject instance.
        /// </summary>
        /// <param name="key">The stored ojects key.</param>
        /// <param name="defaultValue">The default value.</param>
        public StoredObject(string key, T defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;

            // load the value
            _value = Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deletes the stored object and releases the key.
        /// </summary>
        public void Delete()
        {
            _localSettings.Values.Remove(_key);
        }

        /// <summary>
        /// Returns a string that represents the stored object.
        /// </summary>
        /// <returns>The stored objects string.</returns>
        public override string ToString()
        {
            return this._key
                + " with value: " + _value.ToString()
                + ", default value: " + _defaultValue.ToString();
        }

        /// <summary>
        /// Invalidates the value and foreces a refresh from isolated storage the next time.
        /// </summary>
        public void Invalidate()
        {
            this._needRefresh = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get
            {
                if (_needRefresh)
                {
                    object outValue = _defaultValue;

                    // load the value
                    if (_localSettings.Values.TryGetValue(_key, out outValue))
                    {
                        _value = (T)outValue;
                    }
                    else
                    {
                        _value = _defaultValue;
                    }

                    _needRefresh = false;
                }

                return _value;
            }
            set
            {
                if (_value != null && _value.Equals(value))
                    return;

                // store the value in isolated storage
                _localSettings.Values[_key] = value;
                _value = value;
            }
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        public T DefaultValue
        {
            get
            {
                return _defaultValue;
            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key
        {
            get
            {
                return _key;
            }
        }

        #endregion
    }

}
