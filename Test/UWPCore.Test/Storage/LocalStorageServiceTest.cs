using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.IO;
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
        /// The used test text file which is nested in some folders.
        /// </summary>
        public const string TEST_FILE_TXT_WITH_DIRS = "dir1/dir2/" + TEST_FILE_TXT;

        /// <summary>
        /// The used test folder name in root folder.
        /// </summary>
        public const string TEST_FOLDER = "test_folder/";

        /// <summary>
        /// The used nested test folder path starting from root folder.
        /// </summary>
        public const string TEST_FOLDER_NESTED = "dir1/dir2/" + TEST_FOLDER;

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
        public async Task TestWriteInSubfolderCreatesNewFile()
        {
            string data = "data";
            await _storageService.WriteFile(TEST_FILE_TXT_WITH_DIRS, data);

            var fileExists = await _storageService.ContainsFile(TEST_FILE_TXT_WITH_DIRS);

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
            var dirExists1 = await _storageService.ContainsDirectory(@"NonexistingDir/");
            var dirExists2 = await _storageService.ContainsDirectory(@"/NonexistingDirRooted/");
            var dirExists3 = await _storageService.ContainsDirectory(@"NonexistingDir/NonExistingSubDir/");

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

        [TestMethod]
        public async Task TestCreateOrGetFile()
        {
            var file = await _storageService.CreateOrGetFileAsync(TEST_FILE_TXT);

            Assert.IsNotNull(file);
            Assert.AreEqual(TEST_FILE_TXT, file.Name);
        }

        [TestMethod]
        public async Task TestCreateOrGetFileWithDirs()
        {
            var file = await _storageService.CreateOrGetFileAsync(TEST_FILE_TXT_WITH_DIRS);

            Assert.IsNotNull(file);
            Assert.AreEqual(TEST_FILE_TXT, file.Name);
        }

        [TestMethod]
        public async Task TestCreateDirectory()
        {
            var folder = await _storageService.CreateOrGetFolderAsync(TEST_FOLDER);

            Assert.IsNotNull(folder);
            Assert.AreEqual(Path.GetDirectoryName(TEST_FOLDER), folder.Name);
        }

        [TestMethod]
        public async Task TestCreateNestedDirectories()
        {
            var folder = await _storageService.CreateOrGetFolderAsync(TEST_FOLDER_NESTED);

            Assert.IsNotNull(folder);
            Assert.AreEqual(Path.GetDirectoryName(TEST_FOLDER), folder.Name);
        }

        [TestMethod]
        public async Task TestCreateNestedDirectoriesAndDeleteAllFromTheMiddle()
        {
            var folder = await _storageService.CreateOrGetFolderAsync("f1/f2/f3/");

            Assert.IsNotNull(folder);
            Assert.AreEqual("f3", folder.Name);

            var containsFolderF2 = await _storageService.ContainsDirectory("f1/f2/");

            Assert.IsTrue(containsFolderF2);

            await _storageService.DeleteFolderAsync("f1/f2/");

            var containsFolderF1 = await _storageService.ContainsDirectory("f1/");
            containsFolderF2 = await _storageService.ContainsDirectory("f1/f2/");
            var containsFolderF3 = await _storageService.ContainsDirectory("f1/f2/f3/");

            Assert.IsTrue(containsFolderF1);
            Assert.IsFalse(containsFolderF2);
            Assert.IsFalse(containsFolderF3);
        }

        [TestMethod]
        public async Task TestGetFileFromApplicationUri()
        {
            string filePath = IOConstants.APPX_SCHEME + "/Assets/StoreLogo.png";
            var file = await _storageService.GetFileFromApplicationAsync(new Uri(filePath));

            Assert.IsNotNull(file);
            Assert.AreEqual("StoreLogo.png", file.Name);
        }

        [TestMethod]
        public async Task TestGetFileFromApplicationPath()
        {
            var file = await _storageService.GetFileFromApplicationAsync("/Assets/StoreLogo.png");

            Assert.IsNotNull(file);
            Assert.AreEqual("StoreLogo.png", file.Name);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _storageService.DeleteFileAsync(TEST_FILE_TXT);
            await _storageService.DeleteFileAsync(TEST_FILE_TXT_WITH_DIRS);
            await _storageService.DeleteFolderAsync(TEST_FOLDER);
            await _storageService.DeleteFolderAsync(TEST_FOLDER_NESTED);
            await _storageService.DeleteFolderAsync(TEST_FOLDER_NESTED);
        }
    }

}
