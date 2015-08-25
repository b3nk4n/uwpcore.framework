using System.Xml.Linq;
using Windows.Data.Xml.Dom;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Model class for an adaptive tile notification.
    /// </summary>
    /// <remarks>
    /// Template schema documented under <see cref="http://blogs.msdn.com/b/tiles_and_toasts/archive/2015/06/30/adaptive-tile-templates-schema-and-documentation.aspx"/>.
    /// </remarks>
    public class AdaptiveTileModel : IAdaptive , IAdaptiveNotification
    {
        /// <summary>
        /// Gets or sets the visual element.
        /// </summary>
        public AdaptiveVisual Visual { get; set; }

        public XElement GetXElement()
        {
            return new XElement("tile", new XAttribute("version", "3"), Visual.GetXElement());
        }

        public XmlDocument GetXmlDocument()
        {
            XDocument document = new XDocument(GetXElement());
            document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            var xmlDoc = new XmlDocument();
            var xmlAsString = document.ToString();
            xmlDoc.LoadXml(document.ToString());
            return xmlDoc;
        }
    }
}
