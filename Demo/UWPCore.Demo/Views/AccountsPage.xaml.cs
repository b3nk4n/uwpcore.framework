using UWPCore.Framework.Accounts;
using UWPCore.Framework.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountsPage : UniversalPage
    {
        private IUserInfoService _userInfoService;
        private IOnlineIdService _onlineIdService;

        public AccountsPage()
        {
            InitializeComponent();

            _userInfoService = Injector.Get<IUserInfoService>();
            _onlineIdService = Injector.Get<IOnlineIdService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NonRoamableId.Text = await _userInfoService.GetNonRoamableId();
            FirstNameTextBox.Text = await _userInfoService.GetFirstNameAsync();
            LastNameTextBox.Text = await _userInfoService.GetLastNameAsync();
            FullNameTextBox.Text = await _userInfoService.GetFullNameAsync();

            ProfilePicture64Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size64x64);
            ProfilePicture208Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size208x208);
            ProfilePicture424Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size424x424);
            ProfilePicture1080Image.Source = await _userInfoService.GetProfilePictureAsync(Windows.System.UserPictureSize.Size1080x1080);
        }

        private async void LogInOnline(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!_onlineIdService.IsLoggedIn)
            {
                if (await _onlineIdService.AuthenticateAsync(OnlineIdService.SERVICE_SIGNIN))
                {
                    OnlineLiveId.Text = _onlineIdService.UserIdentity.SafeCustomerId;
                }
            }
        }
    }
}
