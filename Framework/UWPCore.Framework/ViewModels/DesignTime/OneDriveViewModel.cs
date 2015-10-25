using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OneDrive.Sdk;
using Windows.UI.Xaml;
using UWPCore.Framework.ViewModels.Interfaces;

namespace UWPCore.Framework.ViewModels.DesignTime
{
    public class OneDriveViewModel : IOneDriveViewModel
    {
        #region Properties

        public Visibility SignInPaneVisibility { get; set; }

        public string UploadPath { get; set; }

        public string UploadName { get; set; }

        public string DataPath { get; set; }

        public string BackupPath { get; set; }

        public string SignInStatusText { get; private set; }

        public bool SignInIsIndeterminate { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string UploadValidationText { get; private set; }

        public string BackupStatusText { get; private set; }

        public bool BackupIsIndeterminate { get; private set; }

        public Item SelectedOneDriveItem { get; set; }

        public ObservableCollection<Item> OneDriveItems { get; }

        #endregion

        #region Constructors

        public OneDriveViewModel()
        {
            SignInPaneVisibility = Visibility.Collapsed;
            SignInStatusText = "This is the sign in status.";
            SignInIsIndeterminate = false;

            BackupPath = @"Apps/UWPCore.Demo/Backup/";
            UploadName = "backup1";
            UploadPath = $@"{BackupPath}{UploadName}.bak";
            UploadValidationText = "Validation Text.";

            BackupStatusText = "This is the backup status.";
            BackupIsIndeterminate = false;

            OneDriveItems = new ObservableCollection<Item>();
            OneDriveItems.Add(new Item { Name = "backup1.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 500 });
            OneDriveItems.Add(new Item { Name = "backup2.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5555 });
            OneDriveItems.Add(new Item { Name = "backup3.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5 });
            OneDriveItems.Add(new Item { Name = "backup4.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 55 });
            SelectedOneDriveItem = OneDriveItems.First();

            OneDriveItems.Add(new Item { Name = "backup1.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 500 });
            OneDriveItems.Add(new Item { Name = "backup2.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5555 });
            OneDriveItems.Add(new Item { Name = "backup3.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5 });
            OneDriveItems.Add(new Item { Name = "backup4.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 55 });

            OneDriveItems.Add(new Item { Name = "backup1.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 500 });
            OneDriveItems.Add(new Item { Name = "backup2.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5555 });
            OneDriveItems.Add(new Item { Name = "backup3.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 5 });
            OneDriveItems.Add(new Item { Name = "backup4.bak", LastModifiedDateTime = DateTimeOffset.Now, Size = 55 });
        }

        #endregion
    }
}
