using Ninject;
using UWPCore.Framework.Data;
using UWPCore.Framework.Storage;

namespace UWPCore.Test.Data
{
    public class CustomCacheService<T> : CacheService<T>
    {
        [Inject]
        public CustomCacheService(IStorageService storageService, ISerializationService serializationService) 
            : base(storageService, serializationService)
        {

        }
    }
}
