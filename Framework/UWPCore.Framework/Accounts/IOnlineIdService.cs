using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace UWPCore.Framework.Accounts
{
    public interface IOnlineIdService
    {
        UserIdentity UserIdentity { get; }

        Task<bool> AuthenticateAsync(params string[] services);

        bool IsLoggedIn { get; }
    }
}
