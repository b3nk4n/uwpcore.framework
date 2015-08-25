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
    /// <remarks>
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx"/> for further information about each template.
    /// </remarks>
    public class TileService : ITileService
    {
        /// <summary>
        /// The text node.
        /// </summary>
        private const string TEXT_NODE = "text";

        /// <summary>
        /// The image node.
        /// </summary>
        private const string IMAGE_NODE = "image";

        public TileNotification CreateTileSquareBlock(string text1Block, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Block);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Block));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareText01(string text1Large, string text2 = "", string text3 = "", string text4 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareText02(string text1Large, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareText03(string text1, string text2 = "", string text3 = "", string text4 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareText04(string text1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text04);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareImage(string imgUri)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquarePeekImageAndText01(string imgUri, string text1Large = "", string text2 = "", string text3 = "", string text4 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquarePeekImageAndText02(string imgUri, string text1Large = "", string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquarePeekImageAndText03(string imgUri, string text1 = "", string text2 = "", string text3 = "", string text4 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquarePeekImageAndText04(string imgUri, string text1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText01(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text02);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText03(string text1Large)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText04(string text1Large)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text04);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText05(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text05);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText06(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text06);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));
            textNodes[9].AppendChild(xmlTemplate.CreateTextNode(text10));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText07(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text07);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText08(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text08);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));
            textNodes[9].AppendChild(xmlTemplate.CreateTextNode(text10));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText09(string text1Large, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text09);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText10(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text10);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideText11(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text11);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));
            textNodes[9].AppendChild(xmlTemplate.CreateTextNode(text10));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideBlockAndText01(string text1, string text2 = "", string text3 = "", string text4 = "", string text5Block = "", string text6UnderBlock = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText01);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5Block));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6UnderBlock));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideBlockAndText02(string text1, string text2Block = "", string text3UnderBlock = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText02);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2Block));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3UnderBlock));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideImage(string imgUri1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideImageCollection(string imgUri1, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageCollection);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideImageAndText01(string imgUri1, string text1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideImageAndText02(string imgUri1, string text1 = "", string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageAndText02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideSmallImageAndText01(string text1Large, string imgUri1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideSmallImageAndText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string imgUri1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideSmallImageAndText03(string text1, string imgUri1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideSmallImageAndText04(string text1Large, string text2 = "", string imgUri1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText04);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWideSmallImageAndText05(string text1Large, string text2 = "", string imgUri1 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText05);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
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
    }
}
