using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Base class of a repository with default behaviour, which should be sufficient for most use cases.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The ID type.</typeparam>
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IRepositoryItem<TKey>
    {
        private IList<TEntity> _data;

        public bool HasLoaded { get; protected set; }

        public RepositoryBase()
        {
            _data = new List<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _data.Add(entity);
        }

        public TEntity Get(TKey id)
        {
            foreach (var entity in _data)
            {
                if (entity.Id.Equals(id))
                    return entity;
            }

            return default(TEntity);
        }

        public IList<TEntity> GetAll()
        {
            return _data;
        }

        public IList<TKey> GetAllIds()
        {
            var idList = new List<TKey>();

            foreach (var item in GetAll())
            {
                idList.Add(item.Id);
            }

            return idList;
        }

        public bool Contains(TKey id)
        {
            return Get(id) != null;
        }

        public void Remove(TEntity entity)
        {
            Remove(entity.Id);
        }

        public virtual void Remove(TKey id)
        {
            int indexToRemove = -1;
            int index = 0;
            foreach (var entity in _data)
            {
                if (entity.Id.Equals(id))
                {
                    indexToRemove = index;
                    break;
                }
                ++index;
            }

            if (indexToRemove != -1)
                _data.RemoveAt(indexToRemove);
        }

        public virtual void Clear()
        {
            _data.Clear();
        }

        public async Task<bool> Load()
        {
            if (!HasLoaded)
            {
                return await Reload();
            }

            return false;
        }

        public abstract Task<bool> Reload();

        public abstract void Update(TEntity prototype);

        public abstract Task<bool> Save();

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _data.Count > 0;
            }
        }
    }
}
