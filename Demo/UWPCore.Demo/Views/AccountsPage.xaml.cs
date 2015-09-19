using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore.Framework.Accounts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountsPage : Page
    {
        private IUserInfoService _userInfoService;

        public AccountsPage()
        {
            InitializeComponent();

            _userInfoService = new UserInfoService();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            FirstNameTextBox.Text = await _userInfoService.GetFirstNameAsync();
            LastNameTextBox.Text = await _userInfoService.GetLastNameAsync();
            FullNameTextBox.Text = await _userInfoService.GetFullNameAsync();

            ProfilePicture64Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size64x64);
            ProfilePicture208Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size208x208);
            ProfilePicture424Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size424x424);
            ProfilePicture1080Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size1080x1080);
        }
    }
}
