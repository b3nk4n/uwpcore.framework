using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Factory class to create adaptive toast notifications.
    /// </summary>
    public class AdaptiveToastFactory : IAdaptiveToastFactory
    {
        public ToastNotification Create(AdaptiveToastModel toast)
        {
            return new ToastNotification(toast.GetXmlDocument());
        }
    }
}
