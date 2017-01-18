using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UWPCore.Framework.Data;
using UWPCore.Framework.Storage;

namespace UWPCore.Test.Data
{
    [TestClass]
    public class CacheServiceTest
    {
        [DataContract]
        public class Test
        {
            [DataMember]
            public string Name { get; set; }
        }

        [TestMethod]
        public async Task CacheServiceAddTestAsync()
        {
            var cacheService = new CustomCacheService<Test>(new LocalStorageService(), new DataContractSerializationService());

            string expected = "Name";

            var id = Guid.NewGuid();
            cacheService.Add(id, new Test { Name = expected });

            var test = await cacheService.LoadAsync(id);
            Assert.AreEqual(test.Name, expected);
        }
    }
}
