using System;
using System.Collections;

namespace UWPCore.Framework.Collections
{
    /// <summary>
    /// Extension methods for the <see cref="System.Collections.IList"/> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles a list.
        /// </summary>
        /// <typeparam name="T"> The list type.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle(this IList list)
        {
            Random rand = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                object value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
