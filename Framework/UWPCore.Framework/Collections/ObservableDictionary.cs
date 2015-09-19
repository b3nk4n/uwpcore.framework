using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;

namespace UWPCore.Framework.Collections
{
    /// <summary>
    /// A simple implementation of IObservableMap, that supports a recurring usage as a default display model.
    /// </summary>
    public class ObservableDictionary : IObservableMap<string, object>
    {
        /// <summary>
        /// The changed event args of an observable dictionary.
        /// </summary>
        private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
        {
            /// <summary>
            /// Creates an ObservableDictionaryChangedEventArgs instance.
            /// </summary>
            /// <param name="change">The collection change.</param>
            /// <param name="key">The key.</param>
            public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
            {
                CollectionChange = change;
                Key = key;
            }

            /// <summary>
            /// Gets the collection change.
            /// </summary>
            public CollectionChange CollectionChange { get; private set; }

            /// <summary>
            /// Gets the key.
            /// </summary>
            public string Key { get; private set; }
        }

        /// <summary>
        /// The dictionary.
        /// </summary>
        private Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        /// <summary>
        /// The map changed event handler.
        /// </summary>
        public event MapChangedEventHandler<string, object> MapChanged;

        /// <summary>
        /// Invokes the collection changed event.
        /// </summary>
        /// <param name="change">The collection change.</param>
        /// <param name="key">The key.</param>
        private void InvokeMapChanged(CollectionChange change, string key)
        {
            var eventHandler = MapChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new ObservableDictionaryChangedEventArgs(change, key));
            }
        }

        /// <summary>
        /// Adds a value and invokes a change event.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            _dictionary.Add(key, value);
            InvokeMapChanged(CollectionChange.ItemInserted, key);
        }

        /// <summary>
        /// Adds a value and invokes a change event.
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes a key and invokes a change event.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>Returns true when the element was removed, else false.</returns>
        public bool Remove(string key)
        {
            if (_dictionary.Remove(key))
            {
                InvokeMapChanged(CollectionChange.ItemRemoved, key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a key and invokes a change event.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Returns true when the element was removed, else false.</returns>
        public bool Remove(KeyValuePair<string, object> item)
        {
            object currentValue;
            if (_dictionary.TryGetValue(item.Key, out currentValue) &&
                Object.Equals(item.Value, currentValue) && _dictionary.Remove(item.Key))
            {
                InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Accesses the value of a given key.
        /// </summary>
        /// <param name="key">The key index.</param>
        /// <returns>Returns the value.</returns>
        public object this[string key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                _dictionary[key] = value;
                InvokeMapChanged(CollectionChange.ItemChanged, key);
            }
        }

        /// <summary>
        /// Clears the dictionary and fires a change event.
        /// </summary>
        public void Clear()
        {
            var priorKeys = _dictionary.Keys.ToArray();
            _dictionary.Clear();
            foreach (var key in priorKeys)
            {
                InvokeMapChanged(CollectionChange.ItemRemoved, key);
            }
        }

        /// <summary>
        /// Gets a collection of all keys.
        /// </summary>
        public ICollection<string> Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Checks whether a key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Returns true when the key exists, else false.</returns>
        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Tries to get a value.
        /// </summary>
        /// <param name="key">The value</param>
        /// <param name="value">The value as an out parameter.</param>
        /// <returns>Returns true when the key exists, else false.</returns>
        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets all values of the dictionary.
        /// </summary>
        public ICollection<object> Values
        {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// Check whether the key value pair exists.
        /// </summary>
        /// <param name="item">The key value pair item.</param>
        /// <returns>Returns true when the item exists, else false.</returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Gets the number of entries.
        /// </summary>
        public int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Indicates whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the enumerator over key value pairs.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Copies the data to the given array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The array index to start from.</param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            int arraySize = array.Length;
            foreach (var pair in _dictionary)
            {
                if (arrayIndex >= arraySize) break;
                array[arrayIndex++] = pair;
            }
        }
    }

}
