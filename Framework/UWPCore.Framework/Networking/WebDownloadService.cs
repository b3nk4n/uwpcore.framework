using Ninject;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UWPCore.Framework.Storage;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// Service class to load and save files from the web.
    /// </summary>
    public class WebDownloadService : IWebDownloadService
    {
        /// <summary>
        /// The storage service.
        /// </summary>
        private IStorageService _storageService;

        /// <summary>
        /// Creates a WebDownloadService instance.
        /// </summary>
        [Inject]
        public WebDownloadService(ILocalStorageService localStorageService)
        {
            _storageService = localStorageService;
        }

        public async Task<IStorageFile> DownloadAsync(Uri fileUri, string fileName = null, NameCollisionOption option = NameCollisionOption.GenerateUniqueName)
        {
            var file = await _storageService.CreateTempFileAsync();
            var downloader = new BackgroundDownloader();
            var download = downloader.CreateDownload(
                fileUri,
                file);

            var res = await download.StartAsync();

            if (string.IsNullOrEmpty(fileName))
            {
                // Use temp file name by default
                fileName = file.Name;

                // Try to find a suggested file name in the http response headers
                // and rename the temp file before returning if the name is found.
                var info = res.GetResponseInformation();

                if (info.Headers.ContainsKey("Content-Disposition"))
                {
                    var cd = info.Headers["Content-Disposition"];
                    var regEx = new Regex("filename=\"(?<fileNameGroup>.+?)\"");
                    var match = regEx.Match(cd);

                    if (match.Success)
                    {
                        fileName = match.Groups["fileNameGroup"].Value;
                        await file.RenameAsync(fileName, option);
                        return file;
                    }
                }
            }

            await file.RenameAsync(fileName, option);
            return file;
        }
    }
}
