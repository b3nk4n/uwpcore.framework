using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    public interface IAdaptiveToastService
    {
        ToastNotification CreateAdaptiveToast(AdaptiveToast toast);
    }
}
