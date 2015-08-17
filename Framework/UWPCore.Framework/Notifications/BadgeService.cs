using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The badge service to update the badge icons on live tile or lock screen.
    /// </summary>
    public class BadgeService : IBadgeService
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
            var glyphString = glyph.ToString();
            glyphString = char.ToLowerInvariant(glyphString[0]) + glyphString.Substring(1);

            var badgeElement = (XmlElement)xml.SelectSingleNode(BADGE_SELECTOR);
            badgeElement.SetAttribute(VALUE_ATTRIBUTE, glyphString);

            return new BadgeNotification(xml);
        }

        public BadgeUpdater GetBadgeUpdaterForApplication(string applicationId = null)
        {
            return BadgeUpdateManager.CreateBadgeUpdaterForApplication();
        }

        public BadgeUpdater GetBadgeUpdaterForSecondaryTile(string tileId)
        {
            return BadgeUpdateManager.CreateBadgeUpdaterForSecondaryTile(tileId);
        }
    }
}
