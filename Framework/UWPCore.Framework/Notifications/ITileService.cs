using Windows.UI.Notifications;

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
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Block">The block text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareBlock(string text1Block, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareText01(string text1Large, string text2 = "", string text3 = "", string text4 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareText02(string text1Large, string text2 = "");
        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareText03(string text1, string text2 = "", string text3 = "", string text4 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareText04(string text1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareImage(string imgUri);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquarePeekImageAndText01(string imgUri, string text1Large = "", string text2 = "", string text3 = "", string text4 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquarePeekImageAndText02(string imgUri, string text1Large = "", string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquarePeekImageAndText03(string imgUri, string text1 = "", string text2 = "", string text3 = "", string text4 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquarePeekImageAndText04(string imgUri, string text1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText01(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText03(string text1Large);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText04(string text1Large);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText05(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <param name="text10">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText06(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText07(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <param name="text10">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText08(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText09(string text1Large, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText10(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Important: If this template is sent to Windows Phone 8.1, it causes the tile to revert to its default image.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <param name="text10">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideText11(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5Block">The block text.</param>
        /// <param name="text6UnderBlock">The text unter block.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideBlockAndText01(string text1, string text2 = "", string text3 = "", string text4 = "", string text5Block = "", string text6UnderBlock = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="text1">The text.</param>
        /// <param name="text2Block">The block text.</param>
        /// <param name="text3UnderBlock">The text unter block.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideBlockAndText02(string text1, string text2Block = "", string text3UnderBlock = "");

        // TODO: implement create methods for all other templates. Add <remarks> to those that are not supported in Windows Phone...

        /// <summary>
        /// Gets the updater for the applications primary tile.
        /// </summary>
        /// <param name="applicationId">The application ID that is required for background tasks.</param>
        /// <returns>Returns the tile updater.</returns>
        TileUpdater GetUpdaterForApplication(string applicationId = null);

        /// <summary>
        /// Gets the updater for the secondary tile.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the tile updater.</returns>
        TileUpdater GetUpdaterForSecondaryTile(string tileId);
    }
}
