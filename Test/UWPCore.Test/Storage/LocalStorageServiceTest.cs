using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using UWPCore.Framework.Storage;

namespace UWPCore.Test.Storage
{
    [TestClass]
    public class LocalStorageServiceTest
    {
        /// <summary>
        /// The used test text file.
        /// </summary>
        public const string TEST_FILE_TXT = "test_file.txt";

        /// <summary>
        /// The system under test.
        /// </summary>
        private IStorageService _storageService;

        [TestInitialize]
        public void Initialize()
        {
            _storageService = new LocalStorageService();
        }

        [TestMethod]
        public async Task TestDoesNotContainNonExisingFile()
        {
            var fileExists = await _storageService.ContainsFile(@"not_existing.txt");

            Assert.IsFalse(fileExists);
        }

        [TestMethod]
        public async Task TestDoesNotContainNonExisingFileOfSubfolder()
        {
            var fileExists = await _storageService.ContainsFile(@"nonexisting_folder/not_existing.txt");

            Assert.IsFalse(fileExists);
        }

        [TestMethod]
        public async Task TestWriteCreatesNewFile()
        {
            string data = "data";
            await _storageService.WriteFile(TEST_FILE_TXT, data);

            var fileExists = await _storageService.ContainsFile(TEST_FILE_TXT);

            Assert.IsTrue(fileExists);
        }

        [TestMethod]
        public async Task TestWriteToExistingFileOverwritesFile()
        {
            string oldData = "old_data";
            string newData = "new_data";

            await _storageService.WriteFile(TEST_FILE_TXT, oldData);
            await _storageService.WriteFile(TEST_FILE_TXT, newData);
            var readData = await _storageService.ReadFile(TEST_FILE_TXT);

            Assert.AreNotEqual(oldData, readData);
            Assert.AreEqual(newData, readData);
        }

        [TestMethod]
        public async Task TestWriteAndReadFile()
        {
            string data = "data";

            await _storageService.WriteFile(TEST_FILE_TXT, data);
            var readData = await _storageService.ReadFile(TEST_FILE_TXT);

            Assert.AreEqual(data, readData);
        }

        [TestMethod]
        public async Task TestDoesNotContainNonExisingDirectory()
        {
            var dirExists1 = await _storageService.ContainsDirectory(@"NonexistingDir");
            var dirExists2 = await _storageService.ContainsDirectory(@"/NonexistingDirRooted");
            var dirExists3 = await _storageService.ContainsDirectory(@"NonexistingDir/NonExistingSubDir");

            Assert.IsFalse(dirExists1);
            Assert.IsFalse(dirExists2);
            Assert.IsFalse(dirExists3);
        }

        [TestMethod]
        public async Task TestCreateAndDeleteFile()
        {
            string data = "data";
            await _storageService.WriteFile(TEST_FILE_TXT, data);
            var fileExistsAfterCreate = await _storageService.ContainsFile(TEST_FILE_TXT);

            await _storageService.DeleteFileAsync(TEST_FILE_TXT);
            var fileExistsAfterDelete = await _storageService.ContainsFile(TEST_FILE_TXT);

            Assert.IsTrue(fileExistsAfterCreate);
            Assert.IsFalse(fileExistsAfterDelete);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // TODO: cleanup files
        }
    }

}
