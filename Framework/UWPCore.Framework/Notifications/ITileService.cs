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
        TileNotification CreateTileLargeTextList01(string text1Large, string text2, string text3, string text4Large = "", string text5 = "", string text6 = "", string text7Large = "", string text8 = "", string text9 = "");

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
        TileNotification CreateTileLargeTextList02(string text1, string text2 = "", string text3 = "");

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
        TileNotification CreateTileLargeTextList03(string text1Large, string text2, string text3Large = "", string text4 = "", string text5Large = "", string text6 = "");

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
        TileNotification CreateTileLargeBlockAndText01(string text1Large, string text8Block, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text9 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeImage(string imgUri1);

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
        TileNotification CreateTileLargeImageCollection(string imgUri1Large, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

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
        TileNotification CreateTileLargeBlockAndText02(string text1Block, string text2Large, string text3Large = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeImageAndText01(string imgUri1, string text1);

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
        TileNotification CreateTileLargeImageAndText02(string imgUri1, string text1, string text2 = "");

        /// <summary>
        /// Creates a tile notification.
        /// </summary>
        /// <remarks>
        /// Large tiles are NOT supported in Windows Phone.
        /// </remarks>
        /// <param name="imgUri1">The image URI.</param>
        /// <param name="text1">The text.</param>
        /// <returns>Returns the tile notification.</returns>
        TileNotification CreateTileLargeImageAndTextOverlay01(string imgUri1, string text1);

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
        TileNotification CreateTileLargeImageAndTextOverlay02(string imgUri1, string text1Large, string text2 = "");

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
        TileNotification CreateTileLargeImageAndTextOverlay03(string imgUri1, string text1Large, string text2 = "", string text3 = "", string text4 = "");

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
        TileNotification CreateTileLargeImageCollectionAndText01(string imgUri1Large, string text1, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

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
        TileNotification CreateTileLargeImageCollectionAndText02(string imgUri1Large, string text1, string text2, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "");

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
        TileNotification CreateTileLargeSmallImagesAndTextList01(string imgUri1, string text1Large, string text2 = "", string text3 = "", string imgUri2 = "", string text4Large = "", string text5 = "", string text6 = "", string imgUri3 = "", string text7Large = "", string text8 = "", string text9 = "");

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
        TileNotification CreateTileLargeSmallImagesAndTextList02(string imgUri1, string text1, string imgUri2 = "", string text2 = "", string imgUri3 = "", string text3 = "");

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
        TileNotification CreateTileLargeSmallImagesAndTextList03(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "");

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
        TileNotification CreateTileLargeSmallImagesAndTextList04(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "");

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
        TileNotification CreateTileLargeSmallImagesAndTextList05(string text1Large, string imgUri1, string text2, string text3 = "", string imgUri2 = "", string text4 = "", string text5 = "", string imgUri3 = "", string text6 = "", string text7 = "");

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
        TileNotification CreateTileLargeSmallImageAndText01(string imgUri1, string text1Large, string text2 = "", string text3 = "");

        // TODO: WinPhone only tiles

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
    }
}
