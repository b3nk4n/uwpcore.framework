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
        public static void Sort<T>(this Collection<T> observable, Func<T, object> keySelector)// where T : IComparable<T>, IEquatable<T>
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
