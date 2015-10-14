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

            string path = "/asdasd/ajsdhajsdh/ajsdhajhd";

            var x = Path.GetDirectoryName(path);
            var e = Path.GetExtension(path);

            var y = x;
            y = e;
            
            vm = new OneDriveViewModel();
            this.DataContext = vm;
        }

        private void OneDrivePage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void SignOut_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Test_Click(object sender, RoutedEventArgs e)
        {
            //var asdasd = await vm.GetItemByPathAsync("Game/Test/text.txt");
            //var asdasd = await vm.GetItemByPathAsync("Game/Test");


            await vm.Test();            
        }
    }
}
