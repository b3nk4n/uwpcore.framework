using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Interface to create primary tiles from a template.
    /// </summary>
    /// <remarks>
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx"/> for further information about each template.
    /// </remarks>
    public interface ITileFactory
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

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideImage(string imgUri1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideImageCollection(string imgUri1, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideImageAndText01(string imgUri1, string text1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideImageAndText02(string imgUri1, string text1 = "", string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="text1Large">The text.</param>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideSmallImageAndText01(string text1Large, string imgUri1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideSmallImageAndText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string imgUri1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="text1">The text.</param>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideSmallImageAndText03(string text1, string imgUri1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="text1Large">The text.</param>
        /// <param name="text2">The content text.</param>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideSmallImageAndText04(string text1Large, string text2 = "", string imgUri1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Image is NOT show in windows phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The content text.</param>
        /// <param name="imgUri1">The image URI, that is NOT shown on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWideSmallImageAndText05(string text1Large, string text2 = "", string imgUri1 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection01(string imgUri1Large, string text1Large, string text2 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection02(string imgUri1Large, string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection03(string imgUri1Large, string text1Large, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection04(string imgUri1Large, string text1, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <param name="imgUri6NextToText">The image next to text URI, that is not visible on Windows Phone.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection05(string imgUri1Large, string text1Large, string text2 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "", string imgUri6NextToText = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="imgUri4">The image URI.</param>
        /// <param name="imgUri5">The image URI.</param>
        /// <param name="imgUri6NextToText">The image next to text URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageCollection06(string imgUri1Large, string text1Large, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "", string imgUri6NextToText = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageAndText01(string imgUri1, string text1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImageAndText02(string imgUri1, string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage01(string imgUri1, string text1Large, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage02(string imgUri1, string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage03(string imgUri1, string text1Large);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage04(string imgUri1, string text1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2NextToText">The image next to text URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage05(string imgUri1, string text1Large, string text2 = "", string imgUri2NextToText = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="imgUri2NextToText">The image next to text URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileWidePeekImage06(string imgUri1, string text1Large, string imgUri2NextToText = "");


        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        TileNotification CreateTileLargeSquareText01(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText03(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <param name="text20">The text.</param>
        /// <param name="text21">The text.</param>
        /// <param name="text22">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText04(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText05(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <param name="text20">The text.</param>
        /// <param name="text21">The text.</param>
        /// <param name="text22">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText06(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText07(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
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
        /// <param name="text11">The text.</param>
        /// <param name="text12">The text.</param>
        /// <param name="text13">The text.</param>
        /// <param name="text14">The text.</param>
        /// <param name="text15">The text.</param>
        /// <param name="text16">The text.</param>
        /// <param name="text17">The text.</param>
        /// <param name="text18">The text.</param>
        /// <param name="text19">The text.</param>
        /// <param name="text20">The text.</param>
        /// <param name="text21">The text.</param>
        /// <param name="text22">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText08(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2Large">The large text.</param>
        /// <param name="text3Large">The large text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareText09(string text1Large, string text2Large = "", string text3Large = "", string text4 = "", string text5 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4Large">The large text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7Large">The large text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareTextList01(string text1Large, string text2, string text3, string text4Large = "", string text5 = "", string text6 = "", string text7Large = "", string text8 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareTextList02(string text1, string text2 = "", string text3 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3Large">The large text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5Large">The large text.</param>
        /// <param name="text6">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareTextList03(string text1Large, string text2, string text3Large = "", string text4 = "", string text5Large = "", string text6 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text8Block">The block text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareBlockAndText01(string text1Large, string text8Block, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImage(string imgUri1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="imgUri2Left">The left image URI.</param>
        /// <param name="imgUri3LeftCenter">The left-center image URI.</param>
        /// <param name="imgUri4RightCenter">The right-center image URI.</param>
        /// <param name="imgUri5Right">The right image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageCollection(string imgUri1Large, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Block">The block text.</param>
        /// <param name="text2Large">The large text.</param>
        /// <param name="text3Large">The large text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareBlockAndText02(string text1Block, string text2Large, string text3Large = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageAndText01(string imgUri1, string text1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageAndText02(string imgUri1, string text1, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageAndTextOverlay01(string imgUri1, string text1);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageAndTextOverlay02(string imgUri1, string text1Large, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="text4">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageAndTextOverlay03(string imgUri1, string text1Large, string text2 = "", string text3 = "", string text4 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="imgUri2Left">The left image URI.</param>
        /// <param name="imgUri3LeftCenter">The left-center image URI.</param>
        /// <param name="imgUri4RightCenter">The right-center image URI.</param>
        /// <param name="imgUri5Right">The right image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageCollectionAndText01(string imgUri1Large, string text1, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1Large">The large image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2Left">The left image URI.</param>
        /// <param name="imgUri3LeftCenter">The left-center image URI.</param>
        /// <param name="imgUri4RightCenter">The right-center image URI.</param>
        /// <param name="imgUri5Right">The right image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareImageCollectionAndText02(string imgUri1Large, string text1, string text2, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="text4Large">The large text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="text6">The text.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="text7Large">The large text.</param>
        /// <param name="text8">The text.</param>
        /// <param name="text9">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImagesAndTextList01(string imgUri1, string text1Large, string text2 = "", string text3 = "", string imgUri2 = "", string text4Large = "", string text5 = "", string text6 = "", string imgUri3 = "", string text7Large = "", string text8 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="text3">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImagesAndTextList02(string imgUri1, string text1, string imgUri2 = "", string text2 = "", string imgUri3 = "", string text3 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="text3Large">The large text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="text5Large">The large text.</param>
        /// <param name="text6">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImagesAndTextList03(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="text3Large">The large text.</param>
        /// <param name="text4">The text.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="text5Large">The large text.</param>
        /// <param name="text6">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImagesAndTextList04(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="text1Large">The large text.</param>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <param name="imgUri2">The image URI.</param>
        /// <param name="text4">The text.</param>
        /// <param name="text5">The text.</param>
        /// <param name="imgUri3">The image URI.</param>
        /// <param name="text6">The text.</param>
        /// <param name="text7">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImagesAndTextList05(string text1Large, string imgUri1, string text2, string text3 = "", string imgUri2 = "", string text4 = "", string text5 = "", string imgUri3 = "", string text6 = "", string text7 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeSquareSmallImageAndText01(string imgUri1, string text1Large, string text2 = "", string text3 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="imgUri">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSmallSquareImage(string imgUri);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="iconUri">The badge icon.</param>
        /// <param name="value">The badge value.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSmallSquareIconWithBadge(string iconUri, int value);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="iconUri">The badge icon.</param>
        /// <param name="value">The badge value.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareIconWithBadge(string iconUri, int value);

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <param name="iconUri">The badge icon.</param>
        /// <param name="value">The badge value.</param>
        /// <param name="text1Large">The large text.</param>
        /// <param name="text2">The text.</param>
        /// <param name="text3">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileSquareIconWithBadge(string iconUri, int value, string text1Large = "", string text2 = "", string text3 = "");
    }
}
