using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UWPCore.Framework.Storage;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Load or save data.
    /// </summary>
    public class CacheService<T> : ICacheService<T>
    {
        #region Fields

        private readonly IStorageService storageService;
        private readonly ISerializationService serializationService;

        private readonly Dictionary<Guid, T> cache;

        #endregion

        #region Constructors

        [Inject]
        /// <summary>
        /// Initialize a new instance of class <see cref="CacheService{T}"/>.
        /// </summary>
        public CacheService(IStorageService storageService, ISerializationService serializationService)
        {
            cache = new Dictionary<Guid, T>();

            this.storageService = storageService;
            this.serializationService = serializationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save specific data.
        /// </summary>
        /// <param name="guid">Id of data to save.</param>
        /// <returns>Returns save state.</returns>
        public virtual async Task<bool> SaveAsync(Guid guid)
        {
            // save all in dictionary
            if (!cache.ContainsKey(guid))
            {
                return false;
            }

            var json = serializationService.SerializeJson<T>(cache[guid]);
            return await storageService.WriteFile(Path.Combine("Data", $"{guid}.json"), json);
        }

        /// <summary>
        /// Load specific data.
        /// </summary>
        /// <param name="guid">Id of data to load.</param>
        /// <returns>Return data.</returns>
        public virtual async Task<T> LoadAsync(Guid guid)
        {
            // save all in dictionary
            if (!cache.ContainsKey(guid))
            {
                var file = await storageService.GetFileAsync(Path.Combine("Data", $"{guid}.json"));
                var data = serializationService.DeserializeJson<T>(await file.OpenStreamForReadAsync());

                if (data == null)
                {
                    System.Diagnostics.Debug.WriteLine(file.Path + " was not loaded");
                    return await Task.FromResult<T>(default(T));
                }

                cache.Add(guid, data);
            }

            return cache[guid];
        }

        /// <summary>
        /// Load all data.
        /// </summary>
        /// <returns>returns list of data.</returns>
        public virtual async Task<IEnumerable<T>> LoadAsync()
        {
            var result = new List<T>();

            var folder = await storageService.CreateOrGetFolderAsync("Data");
            var folders = await folder.GetFoldersAsync();
            foreach (var f in folders)
            {
                var x = await LoadAsync(new Guid(f.Name));
                result.Add(x);
            }

            return result;
        }

        /// <summary>
        /// Index operator.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns></returns>
        public T this[Guid id]
        {
            get
            {
                if (!cache.ContainsKey(id))
                    return default(T);

                return cache[id];
            }
        }

        /// <summary>
        /// Add data to cache.
        /// </summary>
        /// <param name="guid">Id.</param>
        /// <param name="data">Data.</param>
        public void Add(Guid guid, T data)
        {
            cache.Add(guid, data);
        }

        #endregion
    }
}
