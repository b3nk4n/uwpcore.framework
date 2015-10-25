using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WinStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPCore.Framework.Data;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    class OneDriveViewModel : ViewModelBase
    {
        //https://dev.onedrive.com/misc/path-encoding.htm
        private string[] oneDriveReservedCharacters = new string[] { "/", @"\", "<", ">", "?", ":", "|" };

        private Visibility signUpPaneVisibility;
        private string signInStatusText;
        private bool signInIsIndeterminate;

        private bool backupIsIndeterminate;
        private string backupStatusText;

        public string LocalDataPath { get; set; }




        public string SignInStatusText
        {
            get { return backupStatusText; }
            set { Set(ref backupStatusText, value); }
        }



        public bool SignInIsIndeterminate
        {
            get { return signInIsIndeterminate; }
            set { Set(ref signInIsIndeterminate, value); }
        }
        

        public string BackupPath { get; set; }


        private string uploadName;

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

        private string uploadPath;

        public string UploadPath
        {
            get { return uploadPath; }
            private set { Set(ref uploadPath, value); }
        }
        


        public Visibility SignUpPaneVisibility
        {
            get { return signUpPaneVisibility; }
            set { Set(ref signUpPaneVisibility, value); }
        }

        public string BackupStatusText
        {
            get { return backupStatusText; }
            set { Set(ref backupStatusText, value); }
        }

        public bool BackupIsIndeterminate
        {
            get { return backupIsIndeterminate; }
            set
            {
               var x =  Set(ref backupIsIndeterminate, value);
            }
        }

        public DelegateCommand SignUpRetryCommand { get; private set; }
        public DelegateCommand UploadCommand { get; private set; }
        public DelegateCommand DownloadCommand { get; private set; }



        #region Fields

        OneDriveStorageService service;

        private bool isAuthenticated;

        #endregion

        #region Properties

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { Set(ref isAuthenticated, value); }
        }

        private string uploadValidationText;

        public string UploadValidationText
        {
            get { return uploadValidationText; }
            set { Set(ref uploadValidationText, value); }
        }

        #endregion

        #region Constructors
        ILocalStorageService localStorageService;
        ZipArchiveService zipArchiveService;
        public OneDriveViewModel()
        {
            service = new OneDriveStorageService();

            uploadName = string.Empty;

            SignUpRetryCommand = new DelegateCommand(async () =>
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
                await CreateAndUploadBackup();
                await ListBackupAsync();
            },
            () =>
            {
                // Maybe add length between 3 and 20 
                if (UploadName.Length == 0)
                {
                    UploadValidationText = "Name backup.";
                    return false;
                }

                if (oneDriveReservedCharacters.Count(a => UploadName.Contains(a)) > 0)
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
                if (SelectedOneDriveFile == null)
                    return false;

                return true;
            });


            OneDriveFiles = new ObservableCollection<Item>();
            SelectedOneDriveFile = null;

            IsAuthenticated = false;
            SignUpPaneVisibility = Visibility.Visible;

            SignUpRetryCommand.RaiseCanExecuteChanged();
            UploadCommand.RaiseCanExecuteChanged();

            BackupPath = @"Apps/UWPCore.Demo/Backup/";
            UploadPath = BackupPath;
            UploadName = string.Empty;

            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;

            localStorageService = new LocalStorageService();
            zipArchiveService = new ZipArchiveService(localStorageService);

            LocalDataPath = "Data";
        }

        string debug1;

        public string Debug1
        {
            get { return debug1; }
            set { Set(ref debug1, value); }
        }        

        private async Task CreateAndUploadBackup()
        {
            BackupIsIndeterminate = true;
            BackupStatusText = "Create Backup.";

            var x = await localStorageService.CreateOrGetFolderAsync("Test");


            if (await localStorageService.GetFolderAsync(LocalDataPath) == null)
                throw new Exception("Local data path does not exit.");

            try
            {
                await zipArchiveService.CompressAsync(LocalDataPath, "Data.zip");
            }
            catch
            {
                var dialog = new MessageDialog("Backup connte nicht erstellt werden.");
                await dialog.ShowAsync();

                BackupIsIndeterminate = false;
                BackupStatusText = string.Empty;

                return;
            }

            var file = await localStorageService.GetFileAsync("Data.zip");
            var fstream = await file.OpenStreamForReadAsync();
            
                   
            BackupStatusText = $"Uploading Backup ({fstream.Length} Bytes).";

            bool upload = true;

            //prüfen ob existiert
            var item = await service.GetItemByPathAsync(UploadPath);
            if(item != null)
            {
                var dialog = new MessageDialog($"An backup with the name \"{UploadName}.bak\" already exists.", "OneDrive Backup");
                dialog.Commands.Add(new UICommand("Override", null, "Override"));
                dialog.Commands.Add(new UICommand("Cancel", null, "Cancel"));
                if((await dialog.ShowAsync()).Id.ToString() == "Cancel")
                {
                    upload = false;
                }
            }

            if (upload)
            {

                var stream = await file.OpenStreamForReadAsync();
                var result = await service.UploadFileByPathAsync(UploadPath, stream);
                if (result != null)
                {
                    // make toast
                    var dialog = new MessageDialog("Success");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Failed");
                    await dialog.ShowAsync();
                }
            }

            //BackupStatusText = "Clean up ...";
            //await localStorageService.DeleteFileAsync("Data.zip");

            Debug1 = BackupIsIndeterminate.ToString();

            UploadName = string.Empty;
            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;
        }

        private async Task DownloadBackupAsync()
        {
            BackupIsIndeterminate = true;
            BackupStatusText = "Perpare Download.";

            bool download = true;

            var dialog = new MessageDialog($"Downloading a Backup will replace all local data.", "OneDrive Backup");
            dialog.Commands.Add(new UICommand("Download", null, "Download"));
            dialog.Commands.Add(new UICommand("Cancel", null, "Cancel"));
            if ((await dialog.ShowAsync()).Id.ToString() == "Cancel")
            {
                download = false;
            }
            
            if(download)
            {
                var localBackup = await localStorageService.GetFileAsync("Data.zip");
                if(localBackup == null)
                {
                    try
                    {
                        await zipArchiveService.CompressAsync(LocalDataPath, "Data.zip");
                    }
                    catch
                    {
                        // What to do here?
                    }
                }
                                
                BackupStatusText = $"Download Backup {SelectedOneDriveFile.Size}";

                try {
                    var stream = await service.GetContentByIdAsync(SelectedOneDriveFile.Id);
                    if (stream != null)
                    {
                        // Delete folder 
                        await localStorageService.DeleteFolderAsync(LocalDataPath);
                        var sf = await localStorageService.CreateOrGetFileAsync("Download.zip");

                        using (stream)
                        {
                            using (var aa = await sf.OpenStreamForWriteAsync())
                            {
                                stream.Position = 0;
                                aa.Position = 0;
                                await stream.CopyToAsync(aa);
                            }
                        }

                        try
                        {
                            await zipArchiveService.UncompressAsync("Download.zip");
                        }
                        catch (Exception e)
                        {
                            await localStorageService.DeleteFolderAsync(LocalDataPath);
                            await zipArchiveService.UncompressAsync("Data.zip");

                            var adialog = new MessageDialog("S.O. went wrong old files have been recoverd.");
                            await adialog.ShowAsync();
                        }
                    }
                    else
                    {
                        BackupStatusText = "Could not download file ...";
                        await Task.Delay(2000);
                    }
                }
                catch(Exception e)
                {
                    await new MessageDialog("An error occured while downloading a backup. Please try again later.", "OneDrive Backup").ShowAsync();
                }
            }

            await localStorageService.DeleteFileAsync("Download.zip");

            SelectedOneDriveFile = null;

            BackupIsIndeterminate = false;
            BackupStatusText = string.Empty;
        }

        Item selectedOneDriveFile;
        public Item SelectedOneDriveFile
        {
            get { return selectedOneDriveFile; }
            set { Set(ref selectedOneDriveFile, value);
                DownloadCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<Item> oneDriveFiles;

        public ObservableCollection<Item> OneDriveFiles
        {
            get { return oneDriveFiles; }
            set { oneDriveFiles = value; }
        }


        private async Task ListBackupAsync()
        {
            var children = await service.GetChildrenByPathAsync(BackupPath);
            var result = children.Where(a => Path.GetExtension(a.Name) == ".bak");

            OneDriveFiles.Clear();
            foreach(var item in result)
            {
                OneDriveFiles.Add(item);
            }

            
        }

        #endregion

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

        private async Task AuthenticateAsync()
        {
            SignInIsIndeterminate = true;
            SignInStatusText = "Connecting to OneDrive ...";
            SignUpRetryCommand.RaiseCanExecuteChanged();

            await Task.Delay(1000);

            IsAuthenticated = await service.AuthenticateAsync();
            SignInIsIndeterminate = false;

            if (IsAuthenticated)
            {
                SignUpPaneVisibility = Visibility.Collapsed;
            }
            else
            {
                SignInStatusText = "Could not connect to OneDrive.";
                SignUpRetryCommand.RaiseCanExecuteChanged();
            }
        }

        internal async Task Test()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write("Helloasdasd kasjd kjaldsk jasdl k");
            sw.Write("|||Helloasdasd kasjd kjaldsk jasdl k");
            sw.Flush();

            var item = await service.UploadFileByPathAsync("Game/Test23/aaaa/test2.txt", ms);

            var stream = await service.DownloadFileByPathAsync("Game/Test23/aaaa/test2.txt");
            StreamReader sr = new StreamReader(stream);
            var str = sr.ReadToEnd();
        }
    }
}
