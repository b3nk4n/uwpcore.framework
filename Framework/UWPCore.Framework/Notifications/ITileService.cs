using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The interface for the live tile service.
    /// </summary>
    public interface ITileService
    {
        TileNotification CreateTileSquareBlock(string text1Block, string text2 = "");

        TileNotification CreateTileSquareText01(string text1Large, string text2 = "", string text3 = "", string text4 = "");

        TileNotification CreateTileSquareText02(string text1Large, string text2 = "");

        TileNotification CreateTileSquareText03(string text1, string text2 = "", string text3 = "", string text4 = "");

        TileNotification CreateTileSquareText04(string text1);

        TileNotification CreateTileSquareImage(string imgUri);

        TileNotification CreateTileSquarePeekImageAndText01(string imgUri, string text1Large = "", string text2 = "", string text3 = "", string text4 = "");

        TileNotification CreateTileSquarePeekImageAndText02(string imgUri, string text1Large = "", string text2 = "");

        TileNotification CreateTileSquarePeekImageAndText03(string imgUri, string text1 = "", string text2 = "", string text3 = "", string text4 = "");

        TileNotification CreateTileSquarePeekImageAndText04(string imgUri, string text1 = "");

        TileNotification CreateTileWideText01(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        TileNotification CreateTileWideText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        TileNotification CreateTileWideText03(string text1Large);

        TileNotification CreateTileWideText04(string text1Large);

        TileNotification CreateTileWideText05(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        TileNotification CreateTileWideText06(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        TileNotification CreateTileWideText07(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        TileNotification CreateTileWideText08(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        TileNotification CreateTileWideText09(string text1Large, string text2 = "");

        TileNotification CreateTileWideText10(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "");

        TileNotification CreateTileWideText11(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        TileNotification CreateTileWideBlockAndText01(string text1, string text2 = "", string text3 = "", string text4 = "", string text5Block = "", string text6UnderBlock = "");

        TileNotification CreateTileWideBlockAndText02(string text1, string text2Block = "", string text3UnderBlock = "");

        // TODO: implement create methods for all other templates...
        // TODO: add comments
        // TODO: add remarks on those factory methods that are NOT supported in WP / Windows

        TileUpdater GetUpdaterForApplication(string applicationId = null);

        TileUpdater GetUpdaterForSecondaryTile(string tileId);
    }
}
