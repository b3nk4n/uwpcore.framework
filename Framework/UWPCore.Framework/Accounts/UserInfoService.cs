using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPCore.Framework.Accounts
{
    /// <summary>
    /// Service class to recieve user information about the current user.
    /// Remember to define the capability: User Account Information.
    /// A popup will be shown, when a user information is accessed the first time.
    /// </summary>
    public class UserInfoService : IUserInfoService
    {
        /// <summary>
        /// The cached current user.
        /// </summary>
        private User _currentUser;

        /// <summary>
        /// The cached first name.
        /// </summary>
        private string _firstName;

        /// <summary>
        /// The cached last name.
        /// </summary>
        private string _lastName;

        public async Task<string> GetFirstNameAsync()
        {
            if (string.IsNullOrEmpty(_firstName))
            {
                var user = await GetCurrentUserAsync();
                _firstName = (string)await user.GetPropertyAsync(KnownUserProperties.FirstName);
            }

            return _firstName;
        }

        public async Task<string> GetLastNameAsync()
        {
            if (string.IsNullOrEmpty(_lastName))
            {
                var user = await GetCurrentUserAsync();
                _lastName = (string)await user.GetPropertyAsync(KnownUserProperties.LastName);
            }

            return _lastName;
        }

        public async Task<string> GetFullNameAsync()
        {
            var firstName = await GetFirstNameAsync();
            var lastName = await GetLastNameAsync();
            return string.Format("{0} {1}", firstName, lastName);
        }

        public async Task<ImageSource> GetProfilePictureAsync(UserPictureSize desiredSize)
        {
            var user = await GetCurrentUserAsync();
            var streamReference = await user.GetPictureAsync(UserPictureSize.Size64x64);
            if (streamReference != null)
            {
                var stream = await streamReference.OpenReadAsync();
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }

            return null;
        }

        public async Task<string> GetNonRoamableId()
        {
            var user = await GetCurrentUserAsync();
            return user.NonRoamableId;
        }

        /// <summary>
        /// Gets or loads the current user.
        /// </summary>
        /// <returns>Gets the current user or NULL in case there is no user.</returns>
        private async Task<User> GetCurrentUserAsync()
        {
            if (_currentUser == null)
            {
                // load current user
                var users = await User.FindAllAsync(UserType.LocalUser);

                if (users.Count > 0)
                    _currentUser = users[0];
            }

            return _currentUser;
        }
    }
}
