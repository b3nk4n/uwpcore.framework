using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Notifications.Models;
using Windows.Foundation;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.StartScreen;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The interface for the live tile service.
    /// </summary>
    /// <remarks>
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx"/> for further information about each template.
    /// </remarks>
    public interface ITileService
    {
        /// <summary>
        /// Gets the updater for the applications primary tile.
        /// </summary>
        /// <returns>Returns the tile updater.</returns>
        TileUpdater GetUpdaterForApplication();

        /// <summary>
        /// Gets the updater for the secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the tile updater.</returns>
        TileUpdater GetUpdaterForSecondaryTile(string tileId);

        /// <summary>
        /// Creates and pins a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <param name="tileInfo">The secondary tile model.</param>
        /// <param name="arguments">The optional arguments.</param>
        /// <returns>Returns true for success, else false when the tile already exists or in case of an error.</returns>
        Task<bool> PinAsync(string tileId, SecondaryTileModel tileInfo, string arguments = "");

        /// <summary>
        /// Creates and pins a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <param name="tileInfo">The secondary tile model.</param>
        /// <param name="arguments">The optional arguments.</param>
        /// <returns>Returns true for success, else false when the tile already exists or in case of an error.</returns>
        Task<bool> PinForSelectionAsync(string tileId, SecondaryTileModel tileModel, string arguments = "");

        /// <summary>
        /// Updates a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <param name="tileModel">The secondary tile model.</param>
        /// <returns>Returns true for success, else false when the tile does not exists or in case of an error.</returns>
        Task<bool> UpdateAsync(string tileId, SecondaryTileModel tileModel);

        /// <summary>
        /// Checks whether the secondary tile exists on the start screen.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the tile ID.</returns>
        bool Exists(string tileId);

        /// <summary>
        /// Gets all secondary tiles.
        /// </summary>
        /// <returns>Returns all secondary tiles.</returns>
        Task<IReadOnlyList<SecondaryTile>> GetAllSecondaryTilesAsync();

        /// <summary>
        /// Gets the secondary tile.
        /// </summary>
        /// <remarks>
        /// Secondary tiles have to be pinned before.
        /// </remarks>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the secondary tile.</returns>
        Task<SecondaryTile> GetSecondaryTileAsync(string tileId);

        /// <summary>
        /// Unpins a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> UnpinAsync(string tileId);

        /// <summary>
        /// Unpins a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <param name="selection">The selection for the flyout.</param>
        /// <param name="preferredPlacement">The prefered flyout placement.</param>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> UnpinForSelectionAsync(string tileId, Rect selection, Placement preferredPlacement = Placement.Above);

        /// <summary>
        /// Gets the tile factory.
        /// </summary>
        ITileFactory Factory { get; }

        /// <summary>
        /// Gets the adaptive tile factory.
        /// </summary>
        IAdaptiveTileFactory AdaptiveFactory { get; }
    }
}
