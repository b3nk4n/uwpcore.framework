using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Interface for an implementation of the repository pattern
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The ID type.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : IRepositoryItem<TKey>
    {
        /// <summary>
        /// Adds an entitiy to the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>Returns the entity or NULL.</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Gets all entities, but as a copy to be able to manipulate the list.
        /// </summary>
        /// <returns>Returns all entities.</returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns>Returns all keys.</returns>
        IList<TKey> GetAllIds();

        /// <summary>
        /// Checks whether the entity exists.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>Returns True when it exists, else False.</returns>
        bool Contains(TKey id);

        /// <summary>
        /// Updates the given entity using a prototype. All non-NULL values will be used.
        /// </summary>
        /// <param name="prototype">The update-prototype.</param>
        void Update(TEntity prototype);

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="id">The ID.</param>
        void Remove(TKey id);

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Clears all data from the repository.
        /// </summary>
        void Clear();

        /// <summary>
        /// Saves the repository data to disk.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> Save();

        /// <summary>
        /// Loads the repository data from disk, but only when the data has not already been loaded.
        /// </summary>
        /// <returns>Returns True for success, else False when data has already been loaded or in case of an error.</returns>
        Task<bool> Load();

        /// <summary>
        /// Reloads the repository data from disk.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> Reload();

        /// <summary>
        /// Gets whether the repository data has been loaded from disk.
        /// </summary>
        bool HasLoaded { get; }

        /// <summary>
        /// Gets whether the repository is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the data count of the repository.
        /// </summary>
        int Count { get; }
    }
}
