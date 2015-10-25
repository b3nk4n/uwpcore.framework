using Microsoft.OneDrive.Sdk;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace UWPCore.Framework.ViewModels.Interfaces
{
    interface IOneDriveViewModel
    {
        #region Properties

        /// <summary>
        /// Get or set the local data path. The paths root folder is the LocalState folder of the app.
        /// Do not left empty incase of infinity recursions if backup is created.  
        /// </summary>
        string DataPath { get; set; }

        /// <summary>
        /// Get or set the OneDrive backup path. The paths root folder is the OneDrive root folder.
        /// </summary>
        string BackupPath { get; set; }

        /// <summary>
        /// Get or set the visibility of the sign in pane.
        /// </summary>
        Visibility SignInPaneVisibility { get; }

        /// <summary>
        /// Get or set the sign in status.
        /// </summary>
        string SignInStatusText { get; }

        /// <summary>
        /// Get or set if sign in is in progress.
        /// </summary>
        bool SignInIsIndeterminate { get; }

        /// <summary>
        /// Get or set if user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Get or set the name of the backup.
        /// </summary>
        string UploadName { get; set; }

        /// <summary>
        /// Get or set the absolute path of the backup within the OneDrive root.
        /// </summary>
        string UploadPath { get; }

        /// <summary>
        /// Get or set a hint of invalid upload name.
        /// </summary>
        string UploadValidationText { get; }

        /// <summary>
        /// Get or set the status text of the progress of a backup.
        /// </summary>
        string BackupStatusText { get; }

        /// <summary>
        /// Get or set the progress of the backup.
        /// </summary>
        bool BackupIsIndeterminate { get; }

        /// <summary>
        /// Get or set the selected backup item.
        /// </summary>
        Item SelectedOneDriveItem { get; set; }

        /// <summary>
        /// Get a list of selected backup items.
        /// </summary>
        ObservableCollection<Item> OneDriveItems { get; }

        #endregion
    }
}
