using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Factory class to create primary tiles from fixed templates..
    /// </summary>
    /// <remarks>
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx"/> for further information about each template.
    /// </remarks>
    public class TileFactory : ITileFactory
    {
        /// <summary>
        /// The text node.
        /// </summary>
        private const string TEXT_NODE = "text";

        /// <summary>
        /// The image node.
        /// </summary>
        private const string IMAGE_NODE = "image";

        /// <summary>
        /// The badge node (in Windows Phone only templates).
        /// </summary>
        private const string BADGE_NODE = "badge";

        /// <summary>
        /// Creates a TileFactory instance.
        /// </summary>
        public TileFactory()
        {
        }

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

        public TileNotification CreateTileWidePeekImageCollection01(string imgUri1Large, string text1Large, string text2 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageCollection02(string imgUri1Large, string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageCollection03(string imgUri1Large, string text1Large, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageCollection04(string imgUri1Large, string text1, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection04);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageCollection05(string imgUri1Large, string text1Large, string text2 = "", string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "", string imgUri6NextToText = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection05);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;
            imageNodes[5].Attributes[1].NodeValue = imgUri6NextToText;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageCollection06(string imgUri1Large, string text1Large, string imgUri2 = "", string imgUri3 = "", string imgUri4 = "", string imgUri5 = "", string imgUri6NextToText = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageCollection06);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;
            imageNodes[3].Attributes[1].NodeValue = imgUri4;
            imageNodes[4].Attributes[1].NodeValue = imgUri5;
            imageNodes[5].Attributes[1].NodeValue = imgUri6NextToText;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageAndText01(string imgUri1, string text1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImageAndText02(string imgUri1, string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImage01(string imgUri1, string text1Large, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImage02(string imgUri1, string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage02);

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

        public TileNotification CreateTileWidePeekImage03(string imgUri1, string text1Large)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImage04(string imgUri1, string text1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage04);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImage05(string imgUri1, string text1Large, string text2 = "", string imgUri2NextToText = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2NextToText;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileWidePeekImage06(string imgUri1, string text1Large, string imgUri2NextToText = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2NextToText;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText01(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text01);

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

        public TileNotification CreateTileLargeSquareText02(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text02);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText03(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text03);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText04(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text04);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));
            textNodes[19].AppendChild(xmlTemplate.CreateTextNode(text20));
            textNodes[20].AppendChild(xmlTemplate.CreateTextNode(text21));
            textNodes[21].AppendChild(xmlTemplate.CreateTextNode(text22));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText05(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text05);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText06(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text06);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));
            textNodes[19].AppendChild(xmlTemplate.CreateTextNode(text20));
            textNodes[20].AppendChild(xmlTemplate.CreateTextNode(text21));
            textNodes[21].AppendChild(xmlTemplate.CreateTextNode(text22));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText07(string text1Large, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text07);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));

            return new TileNotification(xmlTemplate);

        }

        public TileNotification CreateTileLargeSquareText08(string text1, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text8 = "", string text9 = "", string text10 = "", string text11 = "", string text12 = "", string text13 = "", string text14 = "", string text15 = "", string text16 = "", string text17 = "", string text18 = "", string text19 = "", string text20 = "", string text21 = "", string text22 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text08);

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
            textNodes[10].AppendChild(xmlTemplate.CreateTextNode(text11));
            textNodes[11].AppendChild(xmlTemplate.CreateTextNode(text12));
            textNodes[12].AppendChild(xmlTemplate.CreateTextNode(text13));
            textNodes[13].AppendChild(xmlTemplate.CreateTextNode(text14));
            textNodes[14].AppendChild(xmlTemplate.CreateTextNode(text15));
            textNodes[15].AppendChild(xmlTemplate.CreateTextNode(text16));
            textNodes[16].AppendChild(xmlTemplate.CreateTextNode(text17));
            textNodes[17].AppendChild(xmlTemplate.CreateTextNode(text18));
            textNodes[18].AppendChild(xmlTemplate.CreateTextNode(text19));
            textNodes[19].AppendChild(xmlTemplate.CreateTextNode(text20));
            textNodes[20].AppendChild(xmlTemplate.CreateTextNode(text21));
            textNodes[21].AppendChild(xmlTemplate.CreateTextNode(text22));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareText09(string text1Large, string text2Large = "", string text3Large = "", string text4 = "", string text5 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text09);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2Large));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3Large));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareTextList01(string text1Large, string text2, string text3, string text4Large = "", string text5 = "", string text6 = "", string text7Large = "", string text8 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310TextList01);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4Large));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7Large));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareTextList02(string text1, string text2 = "", string text3 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310TextList02);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareTextList03(string text1Large, string text2, string text3Large = "", string text4 = "", string text5Large = "", string text6 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310TextList03);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3Large));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5Large));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareBlockAndText01(string text1Large, string text8Block, string text2 = "", string text3 = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310BlockAndText01);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8Block));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImage(string imgUri1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Image);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageCollection(string imgUri1Large, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageCollection);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2Left;
            imageNodes[2].Attributes[1].NodeValue = imgUri3LeftCenter;
            imageNodes[3].Attributes[1].NodeValue = imgUri4RightCenter;
            imageNodes[4].Attributes[1].NodeValue = imgUri5Right;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareBlockAndText02(string text1Block, string text2Large, string text3Large = "", string text4 = "", string text5 = "", string text6 = "", string text7 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310BlockAndText02);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Block));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2Large));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3Large));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageAndText01(string imgUri1, string text1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageAndText02(string imgUri1, string text1, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndText02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageAndTextOverlay01(string imgUri1, string text1)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndTextOverlay01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageAndTextOverlay02(string imgUri1, string text1Large, string text2 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndTextOverlay02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageAndTextOverlay03(string imgUri1, string text1Large, string text2 = "", string text3 = "", string text4 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageAndTextOverlay03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageCollectionAndText01(string imgUri1Large, string text1, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageCollectionAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2Left;
            imageNodes[2].Attributes[1].NodeValue = imgUri3LeftCenter;
            imageNodes[3].Attributes[1].NodeValue = imgUri4RightCenter;
            imageNodes[4].Attributes[1].NodeValue = imgUri5Right;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareImageCollectionAndText02(string imgUri1Large, string text1, string text2, string imgUri2Left = "", string imgUri3LeftCenter = "", string imgUri4RightCenter = "", string imgUri5Right = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310ImageCollectionAndText02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1Large;
            imageNodes[1].Attributes[1].NodeValue = imgUri2Left;
            imageNodes[2].Attributes[1].NodeValue = imgUri3LeftCenter;
            imageNodes[3].Attributes[1].NodeValue = imgUri4RightCenter;
            imageNodes[4].Attributes[1].NodeValue = imgUri5Right;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImagesAndTextList01(string imgUri1, string text1Large, string text2 = "", string text3 = "", string imgUri2 = "", string text4Large = "", string text5 = "", string text6 = "", string imgUri3 = "", string text7Large = "", string text8 = "", string text9 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImagesAndTextList01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4Large));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7Large));
            textNodes[7].AppendChild(xmlTemplate.CreateTextNode(text8));
            textNodes[8].AppendChild(xmlTemplate.CreateTextNode(text9));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImagesAndTextList02(string imgUri1, string text1, string imgUri2 = "", string text2 = "", string imgUri3 = "", string text3 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImagesAndTextList02);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImagesAndTextList03(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImagesAndTextList03);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3Large));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5Large));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImagesAndTextList04(string imgUri1, string text1Large, string text2 = "", string imgUri2 = "", string text3Large = "", string text4 = "", string imgUri3 = "", string text5Large = "", string text6 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImagesAndTextList04);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3Large));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5Large));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImagesAndTextList05(string text1Large, string imgUri1, string text2, string text3 = "", string imgUri2 = "", string text4 = "", string text5 = "", string imgUri3 = "", string text6 = "", string text7 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImagesAndTextList05);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;
            imageNodes[1].Attributes[1].NodeValue = imgUri2;
            imageNodes[2].Attributes[1].NodeValue = imgUri3;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));
            textNodes[3].AppendChild(xmlTemplate.CreateTextNode(text4));
            textNodes[4].AppendChild(xmlTemplate.CreateTextNode(text5));
            textNodes[5].AppendChild(xmlTemplate.CreateTextNode(text6));
            textNodes[6].AppendChild(xmlTemplate.CreateTextNode(text7));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileLargeSquareSmallImageAndText01(string imgUri1, string text1Large, string text2 = "", string text3 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310SmallImageAndText01);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri1;

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSmallSquareImage(string imgUri)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare71x71Image);

            var imageNodes = xmlTemplate.GetElementsByTagName(IMAGE_NODE);
            imageNodes[0].Attributes[1].NodeValue = imgUri;

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSmallSquareIconWithBadge(string iconUri, int value)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare71x71IconWithBadge);

            var displayValue = value;
            if (displayValue < 0)
                displayValue = 0;
            else if (displayValue > 100)
                displayValue = 100;

            var badgeNodes = xmlTemplate.GetElementsByTagName(BADGE_NODE);
            badgeNodes[0].Attributes[1].NodeValue = displayValue.ToString();

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareIconWithBadge(string iconUri, int value)
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150IconWithBadge);

            var displayValue = value;
            if (displayValue < 0)
                displayValue = 0;
            else if (displayValue > 100)
                displayValue = 100;

            var badgeNodes = xmlTemplate.GetElementsByTagName(BADGE_NODE);
            badgeNodes[0].Attributes[1].NodeValue = displayValue.ToString();

            return new TileNotification(xmlTemplate);
        }

        public TileNotification CreateTileSquareIconWithBadge(string iconUri, int value, string text1Large = "", string text2 = "", string text3 = "")
        {
            var xmlTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150IconWithBadge);

            var textNodes = xmlTemplate.GetElementsByTagName(TEXT_NODE);
            textNodes[0].AppendChild(xmlTemplate.CreateTextNode(text1Large));
            textNodes[1].AppendChild(xmlTemplate.CreateTextNode(text2));
            textNodes[2].AppendChild(xmlTemplate.CreateTextNode(text3));

            var displayValue = value;
            if (displayValue < 0)
                displayValue = 0;
            else if (displayValue > 100)
                displayValue = 100;

            var badgeNodes = xmlTemplate.GetElementsByTagName(BADGE_NODE);
            badgeNodes[0].Attributes[1].NodeValue = displayValue.ToString();

            return new TileNotification(xmlTemplate);
        }
    }
}
