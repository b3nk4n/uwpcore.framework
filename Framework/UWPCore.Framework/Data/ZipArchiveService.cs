using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Storage;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Zip archive service to zip and unzip data.
    /// </summary>
    public class ZipArchiveService : ICompressionService
    {
        #region Members

        /// <summary>
        /// The local storage service.
        /// </summary>
        IStorageService _localStorageService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize object.
        /// </summary>
        public ZipArchiveService()
        {
            _localStorageService = new LocalStorageService();
        }

        #endregion

        #region Methods

        public async Task CompressAsync(string path, string name = null)
        {
            if (!string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                Logger.WriteLine("Path need to be a folder.");
                return;
            }

            if (!await _localStorageService.ContainsDirectory(path))
            {
                Logger.WriteLine("Folder does not exist.");
                return;
            }

            var zipPath = Path.ChangeExtension(path, ".zip");

            if (!string.IsNullOrEmpty(name))
            {
                var root = Path.GetDirectoryName(zipPath);

                zipPath = Path.Combine(root, name);
                zipPath = Path.ChangeExtension(zipPath, ".zip");
            }

            var file = await _localStorageService.CreateOrReplaceFileAsync(zipPath);
            if (file != null)
            {
                using (var fs = await file.OpenStreamForWriteAsync())
                {
                    using (var zipArchive = new ZipArchive(fs, ZipArchiveMode.Update))
                    {
                        var root = Path.GetDirectoryName(path);
                        var folder = Path.GetFileName(path);

                        await CompressRecursive(zipArchive, root, folder);
                    }
                }
            }
        }

        public async Task UncompressAsync(string path, string name = null)
        {
            var zipFile = await _localStorageService.GetFileAsync(path);
            if (zipFile == null)
            {
                Logger.WriteLine("File does not exist.");
                return;
            }

            using (var ostream = await zipFile.OpenStreamForReadAsync())
            {
                using (var zipArchive = new ZipArchive(ostream))
                {
                    var root = Path.GetDirectoryName(path);
                    foreach (var zipArchiveEntry in zipArchive.Entries)
                    {
                        var unzipPath = Path.Combine(root, zipArchiveEntry.FullName);   // Replace folder name by name.

                        // TODO: Fix bug with path renameing!!
                        if (!string.IsNullOrEmpty(name))
                        {
                            var substring = unzipPath.Remove(0, Path.GetFileNameWithoutExtension(path).Length + 1);
                            unzipPath = Path.Combine(name, substring);
                        }

                        // Create relative path and test if file has extension.
                        if (string.IsNullOrEmpty(Path.GetExtension(unzipPath)))
                        {
                            await _localStorageService.CreateOrReplaceFolderAsync(unzipPath);
                        }
                        else
                        {
                            var file = await _localStorageService.CreateOrReplaceFileAsync(unzipPath);
                            using (var wstream = await file.OpenStreamForWriteAsync())
                            {
                                await zipArchiveEntry.Open().CopyToAsync(wstream);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Helper method of Zip(...), to compress files and folders recursive.
        /// </summary>
        /// <param name="zipArchive">Zip archive to store data in.</param>
        /// <param name="root">Root path to folder to zip (never change).</param>
        /// <param name="path">Current folder to add to zip archive.</param>
        /// <returns>Returns task to await for.</returns>
        private async Task CompressRecursive(ZipArchive zipArchive, string root, string path)
        {
            var folders = await _localStorageService.GetFoldersAsync(Path.Combine(root, path));
            if (folders == null)
                return;

            foreach (var folder in folders)
            {
                var entry = Path.Combine(path, folder.Name);    // Create a new entry to append to archive.
                zipArchive.CreateEntry(string.Format("{0}\\", entry));

                await CompressRecursive(zipArchive, root, entry);
            }

            var files = await _localStorageService.GetFilesAsync(Path.Combine(root, path));
            if (files == null)
                return;

            foreach (var file in files)
            {
                var entry = Path.Combine(path, file.Name);    // Create a new entry to append to archive.
                var zipArchiveEntry = zipArchive.CreateEntry(entry);

                using (var fs = await file.OpenStreamForReadAsync())
                {
                    var stream = zipArchiveEntry.Open();    // Open zip stream to write file stream in.
                    await fs.CopyToAsync(stream);
                }
            }
        }

        #endregion
    }
}
