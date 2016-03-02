using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore.Demo.ViewModels;
using UWPCore.Framework.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPageWithComboBoxStyleAndViewModel : UniversalPage
    {
        public SettingsViewModel ViewModel { get; set; }

        public SettingsPageWithComboBoxStyleAndViewModel()
        {
            InitializeComponent();
            ViewModel = new SettingsViewModel();
            DataContext = ViewModel;
        }
    }
}
