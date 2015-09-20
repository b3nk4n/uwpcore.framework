using Windows.Storage;

namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// Encapsulates a key/value pair stored in a data container.
    /// </summary>
    /// <remarks>
    /// Things to remember:
    /// - The name of each setting can be 255 characters in length at most. Each setting can be up to 8K bytes in size and each composite setting can be up to 64K bytes in size.
    /// - DateTime is NOT working, but DateTimeOffset
    /// </remarks>
    /// <typeparam name="T">The type to store</typeparam>
    public abstract class StoredObjectBase<T>
    {
        #region Members

        /// <summary>
        /// The settings data container.
        /// </summary>
        ApplicationDataContainer _dataContainer;

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
        /// <param name="dataContainer">The data container to use.</param>
        /// <param name="key">The stored ojects key.</param>
        /// <param name="defaultValue">The default value.</param>
        public StoredObjectBase(ApplicationDataContainer dataContainer, string key, T defaultValue)
        {
            _dataContainer = dataContainer;
            _key = key;
            _defaultValue = defaultValue;

            // load the value
            _value = Value;

            if (dataContainer.Locality == ApplicationDataLocality.Roaming)
            {
                ApplicationData.Current.DataChanged += (data, obj) =>
                {
                    Invalidate();
                };
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deletes the stored object and releases the key.
        /// </summary>
        public void Delete()
        {
            _dataContainer.Values.Remove(_key);
        }

        /// <summary>
        /// Returns a string that represents the stored object.
        /// </summary>
        /// <returns>The stored objects string.</returns>
        public override string ToString()
        {
            return _key
                + " with value: " + _value.ToString()
                + ", default value: " + _defaultValue.ToString();
        }

        /// <summary>
        /// Invalidates the value and foreces a refresh from isolated storage the next time.
        /// </summary>
        public void Invalidate()
        {
            _needRefresh = true;
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
                    if (_dataContainer.Values.TryGetValue(_key, out outValue))
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
                _dataContainer.Values[_key] = value;
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
