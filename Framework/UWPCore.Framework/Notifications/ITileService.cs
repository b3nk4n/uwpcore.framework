using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Notifications;
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
        /// <returns>Returns true for success, else false when the tile already exists or in case of an error.</returns>
        Task<bool> PinSecondaryTileAsync(string tileId);

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
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the secondary tile.</returns>
        Task<SecondaryTile> GetSecondaryTileAsync(string tileId);

        /// <summary>
        /// Removes a secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        Task RemoveAsync(string tileId);

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
