using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WinStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore.Demo.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://github.com/OneDrive/onedrive-sdk-csharp/blob/master/docs/auth.md

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OneDrivePage : Page
    {
        OneDriveViewModel vm;
        
        public OneDrivePage()
        {
            this.InitializeComponent();
            this.Loaded += OneDrivePage_Loaded;
            
            vm = new OneDriveViewModel();
            this.DataContext = vm;            
        }

        private void OneDrivePage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
