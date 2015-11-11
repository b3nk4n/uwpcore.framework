using Ninject;
using System;
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
    /// The live tile service.
    /// </summary>
    public class TileService : ITileService
    {
        /// <summary>
        /// The tile factory.
        /// </summary>
        private ITileFactory _tileFactory;

        /// <summary>
        /// The adaptive tile factory.
        /// </summary>
        private IAdaptiveTileFactory _adaptiveTileFactory;

        /// <summary>
        /// Creates a TileService instance.
        /// </summary>
        [Inject]
        public TileService(ITileFactory tileFactory, IAdaptiveTileFactory adaptiveTileFactory)
        {
            _tileFactory = tileFactory;
            _adaptiveTileFactory = adaptiveTileFactory;
        }

        public TileUpdater GetUpdaterForApplication()
        {
            return TileUpdateManager.CreateTileUpdaterForApplication();
        }

        public TileUpdater GetUpdaterForSecondaryTile(string tileId)
        {
            return TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileId);
        }

        public async Task<bool> PinAsync(string tileId, SecondaryTileModel tileModel, string arguments = "")
        {
            if (Exists(tileId))
                return false;

            var tile = new SecondaryTile(tileId)
            {
                Arguments = arguments
            };

            tile = UpdateSecondaryTileFromInfos(tile, tileModel);

            var result = await tile.RequestCreateAsync();
            return result;
        }

        public async Task<bool> PinForSelectionAsync(string tileId, SecondaryTileModel tileModel, string arguments = "")
        {
            if (Exists(tileId))
                return false;

            var tile = new SecondaryTile(tileId)
            {
                Arguments = arguments
            };

            tile = UpdateSecondaryTileFromInfos(tile, tileModel);

            var result = await tile.RequestCreateForSelectionAsync(tileModel.Rect(), tileModel.RequestPlacement);
            return result;
        }

        public async Task<bool> UpdateAsync(string tileId, SecondaryTileModel tileModel)
        {
            if (!Exists(tileId))
                return false;

            var tile = await GetSecondaryTileAsync(tileId);

            // update tile
            tile = UpdateSecondaryTileFromInfos(tile, tileModel);
                
            return await tile.UpdateAsync();
        }

        public bool Exists(string tileId)
        {
            return SecondaryTile.Exists(tileId);
        }

        public async Task<IReadOnlyList<SecondaryTile>> GetAllSecondaryTilesAsync()
        {
            return await SecondaryTile.FindAllAsync();
        }

        public async Task<SecondaryTile> GetSecondaryTileAsync(string tileId)
        {
            var secondaryTiles = await SecondaryTile.FindAllAsync();
            foreach (var secondaryTile in secondaryTiles)
            {
                if (secondaryTile.TileId == tileId)
                    return secondaryTile;
            }

            return null;
        }

        public async Task<bool> UnpinAsync(string tileId)
        {
            if (!Exists(tileId))
                return true;

            var secondaryTile = await GetSecondaryTileAsync(tileId);

            // in case the tileId was empty
            if (secondaryTile == null)
                return false;

            return await secondaryTile.RequestDeleteAsync();
        }

        public async Task<bool> UnpinForSelectionAsync(string tileId, Rect selection, Placement preferredPlacement = Placement.Above)
        {
            if (!Exists(tileId))
                return true;

            var secondaryTile = await GetSecondaryTileAsync(tileId);

            // in case the tileId was empty
            if (secondaryTile == null)
                return false;

            return await secondaryTile.RequestDeleteForSelectionAsync(selection, preferredPlacement);
        }

        private static SecondaryTile UpdateSecondaryTileFromInfos(SecondaryTile tile, SecondaryTileModel tileModel)
        {
            if (tileModel.DisplayName != null)
                tile.DisplayName = tileModel.DisplayName;

            if (tileModel.PhoneticName != null)
                tile.PhoneticName = tileModel.PhoneticName;


            if (tileModel.LockScreenDisplayBadgeAndTileText.HasValue)
                tile.LockScreenDisplayBadgeAndTileText = tileModel.LockScreenDisplayBadgeAndTileText.Value;

            if (tileModel.LockScreenBadgeLogo != null)
            {
                tile.LockScreenBadgeLogo = tileModel.LockScreenBadgeLogo;
            }

            if (tileModel.VisualElements.BackgroundColor.HasValue)
                tile.VisualElements.BackgroundColor = tileModel.VisualElements.BackgroundColor.Value;
            if (tileModel.VisualElements.ForegroundText.HasValue)
                tile.VisualElements.ForegroundText = tileModel.VisualElements.ForegroundText.Value;
            if (tileModel.VisualElements.ShowNameOnSquare150x150Logo.HasValue)
                tile.VisualElements.ShowNameOnSquare150x150Logo = tileModel.VisualElements.ShowNameOnSquare150x150Logo.Value;
            if (tileModel.VisualElements.ShowNameOnSquare310x310Logo.HasValue)
                tile.VisualElements.ShowNameOnSquare310x310Logo = tileModel.VisualElements.ShowNameOnSquare310x310Logo.Value;
            if (tileModel.VisualElements.ShowNameOnWide310x150Logo.HasValue)
                tile.VisualElements.ShowNameOnWide310x150Logo = tileModel.VisualElements.ShowNameOnWide310x150Logo.Value;

            if (tileModel.VisualElements.Square150x150Logo != null)
            {
                tile.VisualElements.Square150x150Logo = tileModel.VisualElements.Square150x150Logo;
            }

            if (tileModel.VisualElements.Square310x310Logo != null)
            {
                tile.VisualElements.Square310x310Logo = tileModel.VisualElements.Square310x310Logo;
            }

            if (tileModel.VisualElements.Wide310x150Logo != null)
            {
                tile.VisualElements.Wide310x150Logo = tileModel.VisualElements.Wide310x150Logo;
            }

            return tile;
        }

        public ITileFactory Factory
        {
            get
            {
                return _tileFactory;
            }
        }

        public IAdaptiveTileFactory AdaptiveFactory
        {
            get
            {
                return _adaptiveTileFactory;
            }
        }
    }
}
