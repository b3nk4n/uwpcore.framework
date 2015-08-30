using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// Inferface for factories to create badge notifications based on a template.
    /// </summary>
    /// <remarks>
    /// For further information about the templates refer to <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh779719.aspx"/>.
    /// </remarks>
    public class BadgeFactory : IBadgeFactory
    {
        /// <summary>
        /// The badge node selector. 
        /// </summary>
        private const string BADGE_SELECTOR = "/badge";

        /// <summary>
        /// The badge value attribute.
        /// </summary>
        private const string VALUE_ATTRIBUTE = "value";

        public BadgeNotification CreateBadgeNumber(int value)
        {
            // ensure no negative value
            if (value < 0)
                value = 0;

            var xml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);

            var badgeElement = (XmlElement)xml.SelectSingleNode(BADGE_SELECTOR);
            badgeElement.SetAttribute(VALUE_ATTRIBUTE, value.ToString());

            return new BadgeNotification(xml);
        }

        public BadgeNotification CreateBadgeNumber(BadgeGlyphIcon glyph)
        {
            var xml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);

            // transform first letter to lower case
            var glyphString = glyph.ToString().FirstLetterToLower();

            var badgeElement = (XmlElement)xml.SelectSingleNode(BADGE_SELECTOR);
            badgeElement.SetAttribute(VALUE_ATTRIBUTE, glyphString);

            return new BadgeNotification(xml);
        }
    }
}
