using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Accounts
{
    /// <summary>
    /// Service class to recieve user information about the current user.
    /// </summary>
    public interface IUserInfoService
    {
        /// <summary>
        /// Gets the users first name.
        /// </summary>
        /// <returns>Returns the users first name or NULL.</returns>
        Task<string> GetFirstNameAsync();

        /// <summary>
        /// Gets the users last name.
        /// </summary>
        /// <returns>Returns the users last name or NULL.</returns>
        Task<string> GetLastNameAsync();

        /// <summary>
        /// Gets the users full name.
        /// </summary>
        /// <returns>Returns the users full name or NULL.</returns>
        Task<string> GetFullNameAsync();

        /// <summary>
        /// Gets the users profile picture.
        /// </summary>
        /// <param name="desiredSize">The desired picture size.</param>
        /// <returns>Returns the users profile picutre or NULL.</returns>
        Task<ImageSource> GetProfilePictureAsync(UserPictureSize desiredSize);

        /// <summary>
        /// Gets the users non-roamable ID.
        /// </summary>
        /// <returns>Returns the non-roamable user ID.</returns>
        Task<string> GetNonRoamableId();
    }
}
