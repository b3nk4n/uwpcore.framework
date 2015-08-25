using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Notifications;
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
        private ITileFactory _tileFacoty;

        /// <summary>
        /// Creates a TileService instance.
        /// </summary>
        public TileService()
        {
            _tileFacoty = new TileFactory();
        }

        public TileUpdater GetUpdaterForApplication()
        {
            return TileUpdateManager.CreateTileUpdaterForApplication();
        }

        public TileUpdater GetUpdaterForSecondaryTile(string tileId)
        {
            return TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileId);
        }

        public async Task<bool> PinSecondaryTileAsync(string tileId)
        {
            if(!Exists(tileId))
            {
                var tile = new SecondaryTile(tileId); // TODO: overloaded methods for all constructors... (or use properties because there are more options?)

                //var tile2 = new SecondaryTile(tileId)
                //{
                //    DisplayName = "Record details",
                //    Arguments = "123",
                //    // ...
                //};

                return await tile.RequestCreateAsync();
            }

            return false;
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

        public async Task RemoveAsync(string tileId)
        {
            if (Exists(tileId))
            {
                var secondaryTile = await GetSecondaryTileAsync(tileId);
                await secondaryTile.RequestDeleteAsync();
            }
        }

        public ITileFactory Factory
        {
            get
            {
                return _tileFacoty;
            }
        }
    }
}
