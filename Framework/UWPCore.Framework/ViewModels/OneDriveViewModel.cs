using Microsoft.OneDrive.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UWPCore.Framework.Data;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Storage;
using UWPCore.Framework.ViewModels.Interfaces;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;


namespace UWPCore.Framework.ViewModels
{
    public class OneDriveViewModel : ViewModelBase, IOneDriveViewModel
    {
        #region Fields

        private const string uploadZip = "data.zip";
        private const string downloadZip = "download.zip";

        OneDriveStorageService oneDriveService;
        ILocalStorageService localStorageService;
        ZipArchiveService zipArchiveService;

        private bool isAuthenticated;
        private Visibility signInPaneVisibility;

        private string signInStatusText;
        private bool signInIsIndeterminate;

        private string backupStatusText;
        private bool backupIsIndeterminate;

        private string uploadName;
        private string uploadPath;
        private string uploadValidationText;

        Item selectedOneDriveItem;
        private readonly ObservableCollection<Item> oneDriveItems;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the local data path. The paths root folder is the LocalState folder of the app.
        /// Do not left empty incase of infinity recursions if backup is created.  
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// Get or set the OneDrive backup path. The paths root folder is the OneDrive root folder.
        /// </summary>
        public string BackupPath { get; set; }

        /// <summary>
        /// Get or set the visibility of the sign in pane.
        /// </summary>
        public Visibility SignInPaneVisibility
        {
            get { return signInPaneVisibility; }
            private set { Set(ref signInPaneVisibility, value); }
        }

        /// <summary>
        /// Get or set the sign in status.
        /// </summary>
        public string SignInStatusText
        {
            get { return signInStatusText; }
            private set { Set(ref signInStatusText, value); }
        }

        /// <summary>
        /// Get or set if sign in is in progress.
        /// </summary>
        public bool SignInIsIndeterminate
        {
            get { return signInIsIndeterminate; }
            private set { Set(ref signInIsIndeterminate, value); }
        }

        /// <summary>
        /// Get or set if user is authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            private set { Set(ref isAuthenticated, value); }
        }

        /// <summary>
        /// Get or set the name of the backup.
        /// </summary>
        public string UploadName
        {
            get { return uploadName; }
            set
            {
                if (Set(ref uploadName, value))
                {
                    UploadCommand.RaiseCanExecuteChanged();
                    UploadPath = Path.Combine(BackupPath, value.Length > 0 ? $"{value}.bak" : string.Empty);
                }
            }
        }

        /// <summary>
        /// Get or set the absolute path of the backup within the OneDrive root.
        /// </summary>
        public string UploadPath
        {
            get { return uploadPath; }
            private set { Set(ref uploadPath, value); }
        }

        /// <summary>
        /// Get or set a hint of invalid upload name.
        /// </summary>
        public string UploadValidationText
        {
            get { return uploadValidationText; }
            private set { Set(ref uploadValidationText, value); }
        }

        /// <summary>
        /// Get or set the status text of the progress of a backup.
        /// </summary>
        public string BackupStatusText
        {
            get { return backupStatusText; }
            private set { Set(ref backupStatusText, value); }
        }

        /// <summary>
        /// Get or set the progress of the backup.
        /// </summary>
        public bool BackupIsIndeterminate
        {
            get { return backupIsIndeterminate; }
            private set { Set(ref backupIsIndeterminate, value); }
        }
        
        /// <summary>
        /// Get or set the selected backup item.
        /// </summary>
        public Item SelectedOneDriveItem
        {
            get { return selectedOneDriveItem; }
            set
            {
                Set(ref selectedOneDriveItem, value);
                DownloadCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Get a list of selected backup items.
        /// </summary>
        public ObservableCollection<Item> OneDriveItems
        {
            get { return oneDriveItems; }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Get or set command to sign in.
        /// </summary>
        public DelegateCommand SignInRetryCommand { get; private set; }

        /// <summary>
        /// Get or set command to upload a .buk-file.
        /// </summary>
        public DelegateCommand UploadCommand { get; private set; }

        /// <summary>
        /// Get or set command to download a .buk-file.
        /// </summary>
        public DelegateCommand DownloadCommand { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize object.
        /// </summary>
        public OneDriveViewModel()
        {
            // Init services.
            oneDriveService = new OneDriveStorageService();
            localStorageService = new LocalStorageService();
            zipArchiveService = new ZipArchiveService(localStorageService);

            // Init commands.
            SignInRetryCommand = new DelegateCommand(async () =>
                {
                    await AuthenticateAsync();
                    await ListBackupAsync();
                },
                () =>
                {
                    if (SignInIsIndeterminate)
                        return false;

                    if (IsAuthenticated)
                        return false;
                    return true;
                });

            UploadCommand = new DelegateCommand(async () =>
                {
                    await UploadBackupAsync();
                    await ListBackupAsync();
                },
                () =>
                {
                    // Maybe add length between 3 and 20 
                    if (UploadName.Length == 0)
                    {
                        UploadValidationText = "Your backup needs a name.";
                        return false;
                    }

                    if (oneDriveService.OneDriveReservedCharacters.Count(a => UploadName.Contains(a)) > 0)
                    {
                        UploadValidationText = @"Contains reserved char /\<>:? or |.";
                        return false;
                    }

                    UploadValidationText = string.Empty;
                    return true;
                });


            DownloadCommand = new DelegateCommand(async () =>
                {
                    await DownloadBackupAsync();
                },
                () =>
                {
                    // Maybe add length between 3 and 20 
                    if (SelectedOneDriveItem == null)
                        return false;

                    return true;
                });

            // Init properties.
            IsAuthenticated = false;
            SignInPaneVisibility = Visibility.Visible;
            SignInStatusText = "Could not sign in automatically. Please retry.";

            oneDriveItems = new ObservableCollection<Item>();
            SelectedOneDriveItem = null;

            DataPath = "Data";
            BackupPath = @"Apps/UWPCore.Demo/Backup/";
            UploadPath = BackupPath;
            UploadName = string.Empty;

            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;
            
            SignInRetryCommand.RaiseCanExecuteChanged();
            UploadCommand.RaiseCanExecuteChanged();
            DownloadCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zips the folder to backup and load it to one drive.
        /// </summary>
        /// <returns></returns>
        private async Task UploadBackupAsync()
        {
            BackupIsIndeterminate = true;
            BackupStatusText = "Create backup ...";
                        
            if (await localStorageService.GetFolderAsync(DataPath) == null)
                throw new Exception("Local data path does not exit.");

            try
            {
                await zipArchiveService.CompressAsync(DataPath, uploadZip);
            }
            catch
            {
                await new MessageDialog("Could not create a backup.", "OneDrive Backup").ShowAsync();
                BackupIsIndeterminate = false;
                BackupStatusText = string.Empty;

                return;
            }

            var file = await localStorageService.GetFileAsync(uploadZip);
            using (var stream = await file.OpenStreamForReadAsync())
            {
                BackupStatusText = $"Uploading Backup (total: {stream.Length} bytes) ...";
            }

            bool upload = true;

            try
            {
                // Check if an item with the same name already exists.    
                var item = await oneDriveService.GetItemByPathAsync(UploadPath);
                if (item != null)
                {
                    var dialog = new MessageDialog($"An backup with the name \"{UploadName}.bak\" already exists.", "OneDrive Backup");
                    dialog.Commands.Add(new UICommand("Override", null, "Override"));
                    dialog.Commands.Add(new UICommand("Cancel", null, "Cancel"));
                    if ((await dialog.ShowAsync()).Id.ToString() == "Cancel")
                    {
                        upload = false;
                    }
                }

                if (upload)
                {
                    var stream = await file.OpenStreamForReadAsync();
                    var result = await oneDriveService.UploadFileByPathAsync(UploadPath, stream);
                    if (result != null)
                    {
                        // TODO: Evaluate if using toast notifications instead.
                        await new MessageDialog("The backup was sucessfully created.", "OneDrive Backup").ShowAsync();
                    }
                    else
                    {
                        await new MessageDialog("An error occured while uploading a backup. Please try again later.", "OneDrive Backup").ShowAsync();
                    }
                }
            }
            catch(Exception)
            {
                await new MessageDialog("An error occured while uploading a backup. Please try again later.", "OneDrive Backup").ShowAsync();
            }
            
            UploadName = string.Empty;
            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;
        }

        /// <summary>
        /// Download the selected backup and replace the current data with it.
        /// </summary>
        /// <returns></returns>
        private async Task DownloadBackupAsync()
        {
            BackupIsIndeterminate = true;
            BackupStatusText = "Perpare download ...";

            bool download = true;

            var dialog = new MessageDialog("Downloading a Backup will replace all local data.", "OneDrive Backup");
            dialog.Commands.Add(new UICommand("Download", null, "Download"));
            dialog.Commands.Add(new UICommand("Cancel", null, "Cancel"));
            if ((await dialog.ShowAsync()).Id.ToString() == "Cancel")
            {
                download = false;
            }

            if (download)
            {
                var localBackup = await localStorageService.GetFileAsync(uploadZip);
                if (localBackup == null)
                {
                    try
                    {
                        await zipArchiveService.CompressAsync(DataPath, uploadZip); // TODO: fix deprecation
                    }
                    catch
                    {
                        // TODO: Add error handling.
                    }
                }

                BackupStatusText = $"Download Backup (total: {SelectedOneDriveItem.Size} bytes) ...";

                try
                {
                    var backupStream = await oneDriveService.GetContentByIdAsync(SelectedOneDriveItem.Id);
                    if (backupStream != null)
                    {
                        // Delete folder 
                        await localStorageService.DeleteFolderAsync(DataPath);
                        var folder = await localStorageService.CreateOrGetFileAsync(downloadZip);

                        using (backupStream)
                        {
                            using (var fileStream = await folder.OpenStreamForWriteAsync())
                            {
                                backupStream.Position = 0;
                                fileStream.Position = 0;
                                await backupStream.CopyToAsync(fileStream);
                            }
                        }

                        try
                        {
                            await zipArchiveService.UncompressAsync(downloadZip); // TODO: fix deprecation
                        }
                        catch
                        {
                            await localStorageService.DeleteFolderAsync(DataPath);
                            await zipArchiveService.UncompressAsync(uploadZip); // TODO: fix deprecation

                            await new MessageDialog("An error occured while saving the backup. Old data have been restored.", "OneDrive Backup").ShowAsync();
                        }
                    }
                    else
                    {
                        await new MessageDialog("An error occured while downloading a backup. Please try again later.", "OneDrive Backup").ShowAsync();
                    }
                }
                catch (Exception)
                {
                    await new MessageDialog("An error occured while downloading a backup. Please try again later.", "OneDrive Backup").ShowAsync();
                }
            }

            await localStorageService.DeleteFileAsync(downloadZip);

            SelectedOneDriveItem = null;

            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;
        }

        /// <summary>
        /// Get the actual list of backups that are stored into OneDrive.
        /// </summary>
        /// <returns></returns>
        private async Task ListBackupAsync()
        {
            var children = await oneDriveService.GetChildrenByPathAsync(BackupPath);
            var result = children.Where(a => Path.GetExtension(a.Name) == ".bak");

            OneDriveItems.Clear();
            foreach (var item in result)
            {
                OneDriveItems.Add(item);
            }
        }
        
        /// <summary>
        /// Authenticate the user to OneDrive.
        /// </summary>
        /// <returns></returns>
        private async Task AuthenticateAsync()
        {
            SignInIsIndeterminate = true;
            SignInStatusText = "Connecting to OneDrive ...";
            SignInRetryCommand.RaiseCanExecuteChanged();

            //await Task.Delay(1000);

            IsAuthenticated = await oneDriveService.AuthenticateAsync();
            SignInIsIndeterminate = false;

            if (IsAuthenticated)
            {
                SignInPaneVisibility = Visibility.Collapsed;
            }
            else
            {
                SignInStatusText = "Could not connect to OneDrive.";
                SignInRetryCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Automatic authenticate the user to OneDrive if site is loaded.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);

            Action task = async () =>
            {
                await AuthenticateAsync();
                await ListBackupAsync();
            };
            task();
        }

        #endregion
    }
}
