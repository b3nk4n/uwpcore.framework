using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    public class AdaptiveToastService : IAdaptiveToastService
    {
        public ToastNotification CreateAdaptiveToast(AdaptiveToast toast)
        {
            return new ToastNotification(toast.GetXmlDocument());
        }

        // TODO: implement adatpive toast service
    }
}
