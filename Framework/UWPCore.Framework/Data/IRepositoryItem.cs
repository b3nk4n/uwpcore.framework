namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Interface for model items in the <see cref="IRepository{TEntity, TKey}"/>.
    /// Use <see cref="Nullable{T}"/> wherever possible, which is used by the prototype-update mechanism.
    /// </summary>
    /// <typeparam name="TKey">The type of the ID.</typeparam>
    public interface IRepositoryItem<TKey>
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        TKey Id { get; }
    }
}
