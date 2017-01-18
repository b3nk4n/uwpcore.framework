using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace UWPCore.Framework.Accounts
{
    /// <summary>
    /// Simple service interface for primitive Microsoft account login.
    /// </summary>
    public interface IOnlineIdService
    {
        /// <summary>
        /// Gets the user identity, which is only available after calling <see cref="AuthenticateAsync(string[])"/>.
        /// </summary>
        UserIdentity UserIdentity { get; }

        /// <summary>
        /// Authenticates the user. Might through a prompt.
        /// </summary>
        /// <param name="services">The services/privileges that the app might require.</param>
        /// <returns>True of the login was successfull, else false.</returns>
        Task<bool> AuthenticateAsync(params string[] services);

        /// <summary>
        /// Indicates whether the the user is logged in or not.
        /// </summary>
        bool IsLoggedIn { get; }
    }
}
