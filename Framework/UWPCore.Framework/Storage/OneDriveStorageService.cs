using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WinStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UWPCore.Framework.Storage
{
    [Obsolete("Use on own risk")]
    public class OneDriveStorageService
    {
        #region Fields

        IOneDriveClient oneDriveClient;

        //https://dev.onedrive.com/auth/msa_oauth.htm
        string[] scopes;

        private bool isAuthenticated;

        #endregion

        #region Properties

        //https://dev.onedrive.com/misc/path-encoding.htm
        public readonly string[] OneDriveReservedCharacters = new string[] { "/", @"\", "<", ">", "?", ":", "|" };

        public string[] Scopes
        {
            get { return scopes; }
            set { scopes = value; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            private set { isAuthenticated = value; }
        }

        #endregion

        #region Constructors

        public OneDriveStorageService()
        {
            scopes = new string[] { "wl.signin", "wl.offline_access", "onedrive.readwrite" };
        }

        #endregion

        #region Methods

        public async Task<bool> AuthenticateAsync()
        {
            try
            {
                oneDriveClient = OneDriveClientExtensions.GetUniversalClient(Scopes);
                await oneDriveClient.AuthenticateAsync();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Fail auth. Make sure app is registered in store.");
            }

            try
            {
                // little hack to make shure the service works
                await oneDriveClient.Drive.Request().GetAsync();
                IsAuthenticated = true;
            }
            catch (Exception)
            {
                IsAuthenticated = false;
            }

            return IsAuthenticated;
        }

        private async Task<Item> CreateFolderByPathAsync(string path, string name)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            var folderToCreate = new Item { Name = name, Folder = new Folder(), AdditionalData = new Dictionary<string, object> { ["@name.conflictBehavior"] = "rename" } };

            Item folder = null;
            try
            {
                folder = await oneDriveClient
                                      .Drive
                                      .Root
                                      .ItemWithPath(path)
                                      .Children
                                      .Request()
                                      .AddAsync(folderToCreate);
            }
            catch
            {
                return null;
            }

            return folder;
        }

        public async Task<Item> GetOrCreateFolderAsync(string path)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            if (Path.GetExtension(path) != string.Empty)
                throw new Exception("Not a folder");

            Item folder = await GetItemByPathAsync(path);
            if (folder != null && folder.Folder != null)
                return folder;

            var segments = path.Split('/');
            string tmp = string.Empty;
            foreach (var segment in segments)
            {
                folder = await GetItemByPathAsync(Path.Combine(tmp, segment));
                if (folder == null)
                {
                    folder = await CreateFolderByPathAsync(tmp, segment);
                    if (folder == null)
                        throw new Exception("Foulder could not be created");
                }
                tmp = Path.Combine(tmp, segment);
            }

            return folder;
        }

        public async Task<Item> GetItemByPathAsync(string path)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            Item item = null;

            try
            {
                item = await oneDriveClient.Drive
                                           .Root
                                           .ItemWithPath(path)
                                           .Request()
                                           .GetAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return item;
        }

        public async Task<Stream> DownloadFileByPathAsync(string path)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            Stream content = null;

            try
            {
                content = await oneDriveClient.Drive
                                              .Root
                                              .ItemWithPath(path)
                                              .Content
                                              .Request()
                                              .GetAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return content;
        }

        public async Task<Stream> GetContentByIdAsync(string id)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            Stream content = null;

            try
            {
                content = await oneDriveClient.Drive
                                              .Items[id]
                                              .Content
                                              .Request()
                                              .GetAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return content;
        }

        public async Task<IChildrenCollectionPage> GetChildrenByPathAsync(string path)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            IChildrenCollectionPage children = null;

            try
            {
                children = await oneDriveClient.Drive
                                              .Root
                                              .ItemWithPath(path)
                                              .Children
                                              .Request()
                                              .GetAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return children;
        }

        public async Task<IChildrenCollectionPage> GetChildrenByIdAsync(string id)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            IChildrenCollectionPage children = null;

            try
            {
                children = await oneDriveClient.Drive
                                              .Items[id]
                                              .Children
                                              .Request()
                                              .GetAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return children;
        }

        public async Task<Item> UploadFileByPathAsync(string path, Stream content)
        {
            if (!isAuthenticated)
                throw new Exception("Not authenticated");

            if (Path.GetExtension(path) == string.Empty)
                throw new Exception("Not a file");

            var dir = Path.GetDirectoryName(path);
            if (GetOrCreateFolderAsync(dir) == null)
                throw new Exception("No Folder");

            Item item = null;
            try
            {
                content.Position = 0;
                using (content)
                {
                    item = await oneDriveClient.Drive
                                               .Root
                                               .ItemWithPath(path)
                                               .Content
                                               .Request()
                                               .PutAsync<Item>(content);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return item;
        }

        #endregion
    }
}
