using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Service class for adaptive tiles.
    /// </summary>
    public class AdaptiveTileService : IAdaptiveTileService
    {
        public TileNotification CreateAdaptiveTile(AdaptiveTile tile)
        {
            return new TileNotification(tile.GetXmlDocument());
        }

        // TODO: add methods to create some tiles.
        // because AdaptiveTileService and TileService would use the same methods to update the tiles, maybe it's a good idea to split create methods to a factory class?
    }
}
