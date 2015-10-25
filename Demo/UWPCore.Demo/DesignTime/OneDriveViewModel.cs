using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPCore.Demo.Interfaces;
using Windows.UI.Xaml;

namespace UWPCore.Demo.DesignTime
{
    public class OneDriveViewModel : IOneDriveViewModel
    {
        public OneDriveViewModel()
        {
            SignUpPaneVisibility = Visibility.Collapsed;
            UploadPath = @"Apps/UWPCore.Demo/Backup/"; // BAckupPath;
            UploadName = "backup1";
        }

        public Visibility SignUpPaneVisibility { get; set; }

        public string UploadPath { get; set; }

        public string UploadName { get; set; }
    }
}
