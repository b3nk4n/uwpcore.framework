using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UWPCore.Demo.Interfaces
{
    interface IOneDriveViewModel
    {
        Visibility SignUpPaneVisibility { get; set; }
        string UploadPath { get; set; }
    }
}
