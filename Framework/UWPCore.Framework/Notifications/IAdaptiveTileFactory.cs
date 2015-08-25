using UWPCore.Framework.Notifications.Models;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface for an adaptive tile factory.
    /// </summary>
    public interface IAdaptiveTileFactory
    {
        /// <summary>
        /// Creates an adaptive tile.
        /// </summary>
        /// <param name="tile">The tile model to create.</param>
        /// <returns>The tile notification.</returns>
        TileNotification Create(AdaptiveTileModel tile);
    }
}
