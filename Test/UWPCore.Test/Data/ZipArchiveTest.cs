using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UWPCore.Framework.Storage;
using Windows.Storage;
using UWPCore.Framework.Data;

namespace UWPCore.Test.Data
{
    [TestClass]
    public class ZipArchiveTest
    {
        private readonly IStorageService storageService;

        public ZipArchiveTest()
        {
            storageService = new LocalStorageService();
        }

        [TestInitialize]
        public async Task InitializeAsync()
        {
            await storageService.CreateOrGetFolderAsync(@"Data");
            await InitializeStorageFile(await storageService.CreateOrGetFileAsync(@"Data\data1.txt"));
            await InitializeStorageFile(await storageService.CreateOrGetFileAsync(@"Data\data2.txt"));
            var folder = await storageService.CreateOrGetFolderAsync(@"Data\Test");
            await InitializeStorageFile(await storageService.CreateOrGetFileAsync(@"Data\Test\test1.txt"));
            await InitializeStorageFile(await storageService.CreateOrGetFileAsync(@"Data\Test\test2.txt"));
        }

        private async Task InitializeStorageFile(IStorageFile file)
        {
            string message = "Short message!";
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            }
        }

        [TestMethod]
        public async Task ZipArchiveServiceCompressTestAsync()
        {
            var service = new ZipArchiveService(new LocalStorageService());

            var file = await storageService.CreateOrGetFileAsync("data.zip");
            var folder = await storageService.CreateOrGetFolderAsync("Data");

            var result = await service.CompressAsync(folder, file);
            Assert.IsTrue(result);
            
            // Unzip
            var unzipFolder = await storageService.CreateOrGetFolderAsync("UnzipedData");

            result = await service.UncompressAsync(file, unzipFolder);
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public async Task CleanupAsync()
        {
            IStorageService storageService = new LocalStorageService();
            await storageService.DeleteFolderAsync(@"Data");
        }

    }
}
