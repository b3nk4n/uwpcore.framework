using System.Collections.Generic;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// Extensin methods for <see cref="Queue{T}"/>.
    /// </summary>
    public static class QueueExtensions
    {
        /// <summary>
        /// Returns and removed the couple of entries.
        /// </summary>
        /// <param name="queue">This queue.</param>
        /// <param name="count">The number of entries to take and remove.</param>
        /// <returns>Returns 'count' items maximum.</returns>
        public static IEnumerable<T> TakeAndRemove<T>(this Queue<T> queue, int count)
        {
            for (int i = 0; i < count && queue.Count > 0; i++)
                yield return queue.Dequeue();
        }
    }
}
