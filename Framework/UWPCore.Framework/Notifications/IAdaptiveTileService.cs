using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for an adaptive tile service.
    /// </summary>
    public interface IAdaptiveTileService
    {
        /// <summary>
        /// Creates a simple adaptive tile.
        /// </summary>
        /// <param name="tile">The tile model to create.</param>
        /// <returns>The tile notification.</returns>
        TileNotification CreateAdaptiveTile(AdaptiveTile tile);
    }
}
