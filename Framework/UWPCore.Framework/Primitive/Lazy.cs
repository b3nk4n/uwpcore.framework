using System;

namespace UWPCore.Framework.Primitive
{
    /// <summary>
    /// Simple "Lazy load" type implementation.
    /// </summary>
    /// <typeparam name="T">The type to load lazily</typeparam>
    public class Lazy<T>
    {
        /// <summary>
        /// The synchronization mutex to ensure thread safity.
        /// </summary>
        private readonly object _sync = new object();

        /// <summary>
        /// The get function.
        /// </summary>
        private readonly Func<T> _getter;

        /// <summary>
        /// The lazily loaded instance.
        /// </summary>
        private T _instance;

        /// <summary>
        /// Indicates whether the value has already been loaded.
        /// </summary>
        public bool IsValueCreated { get; private set; }

        /// <summary>
        /// Creates an Lazy instance.
        /// </summary>
        /// <param name="getter">The getter function to create that creates or gets the object.</param>
        public Lazy(Func<T> getter)
        {
            _getter = getter;
        }

        /// <summary>
        /// Gets the value that is loaded lazily.
        /// </summary>
        public T Value
        {
            get
            {
                if (!IsValueCreated)
                {
                    lock (_sync)
                    {
                        if (!IsValueCreated)
                        {
                            _instance = _getter();
                            IsValueCreated = true;
                        }
                    }
                }
                return _instance;
            }
        }
    }

}
