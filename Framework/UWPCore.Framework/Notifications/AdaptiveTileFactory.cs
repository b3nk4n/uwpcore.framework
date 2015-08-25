using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Factory class for adaptive tiles.
    /// </summary>
    public class AdaptiveTileFactory : IAdaptiveTileFactory
    {
        public TileNotification Create(AdaptiveTileModel tile)
        {
            return new TileNotification(tile.GetXmlDocument());
        }
    }
}
