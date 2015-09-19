using UWPCore.Framework.Accounts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
