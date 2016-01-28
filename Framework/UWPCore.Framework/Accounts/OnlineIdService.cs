using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace UWPCore.Framework.Accounts
{
    public class OnlineIdService : IOnlineIdService
    {
        public const string SERVICE_OFFLINE_ACCESS = "wl.offline_access";
        public const string SERVICE_SIGNIN = "wl.signin";
        public const string SERVICE_BASIC = "wl.basic";
        public const string SERVICE_CONTACT_PHOTOS = "wl.contacts_photos";
        public const string SERVICE_CALENDARS = "wl.calendars";

        public const string POLICY_DELEGATION = "DELEGATION";

        public UserIdentity UserIdentity { get; private set; }

        OnlineIdAuthenticator _authenticator;

        public OnlineIdService()
        {
            _authenticator = new OnlineIdAuthenticator();
        }

        public async Task<bool> AuthenticateAsync(params string[] services)
        {
            List<OnlineIdServiceTicketRequest> targetArray = new List<OnlineIdServiceTicketRequest>();
            targetArray.Add(new OnlineIdServiceTicketRequest(string.Join(" ", services), POLICY_DELEGATION));

            try
            {
                UserIdentity = await _authenticator.AuthenticateUserAsync(targetArray, CredentialPromptType.PromptIfNeeded);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return UserIdentity != null;
            }
        }
    }
}
