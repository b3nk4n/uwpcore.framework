using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UWPCore.Framework.Collections
{
    /// <summary>
    /// Extension methods for the <see cref="System.Collections.IList"/> class.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a sort function to <see cref="Collection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="observable">The collection to sort.</param>
        /// <param name="keySelector">The key selector.</param>
        public static void Sort<T>(this Collection<T> observable, Func<T, object> keySelector)
        {
            List<T> sorted = observable.OrderBy(keySelector).ToList();

            int ptr = 0;
            while (ptr < sorted.Count)
            {
                if (!observable[ptr].Equals(sorted[ptr]))
                {
                    T t = observable[ptr];
                    observable.RemoveAt(ptr);
                    observable.Insert(sorted.IndexOf(t), t);
                }
                else
                {
                    ptr++;
                }
            }
        }
    }
}
