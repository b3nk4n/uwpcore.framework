using System;
using UWPCore.Framework.Launcher;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Controls
{
    public sealed partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private async void ThirdPartyItemTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var item = sender as FrameworkElement;

            if (item != null)
            {
                var linkString = (string)item.Tag;
                await SystemLauncher.LaunchUriAsync(new Uri(linkString, UriKind.Absolute));
            }
        }
    }
}
