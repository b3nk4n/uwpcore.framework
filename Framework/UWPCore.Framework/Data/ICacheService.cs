using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWPCore.Framework.Data
{
    public interface ICacheService<T>
    {
        /// <summary>
        /// Save specific data.
        /// </summary>
        /// <param name="guid">Id of data to save.</param>
        /// <returns>Returns save state.</returns>
        Task<bool> SaveAsync(Guid guid);

        /// <summary>
        /// Load specific data.
        /// </summary>
        /// <param name="guid">Id of data to load.</param>
        /// <returns>Return data.</returns>
        Task<T> LoadAsync(Guid guid);

        /// <summary>
        /// Load all data.
        /// </summary>
        /// <returns>returns list of data.</returns>
        Task<IEnumerable<T>> LoadAsync();

        /// <summary>
        /// Index operator.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns></returns>
        T this[Guid id] { get; }

        /// <summary>
        /// Add data to cache.
        /// </summary>
        /// <param name="guid">Id.</param>
        /// <param name="data">Data.</param>
        void Add(Guid guid, T data);
    }
}
