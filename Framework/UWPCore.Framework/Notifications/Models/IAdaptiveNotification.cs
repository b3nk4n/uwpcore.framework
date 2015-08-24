using Windows.Data.Xml.Dom;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Interface for adaptive notification models.
    /// </summary>
    public interface IAdaptiveNotification
    {
        /// <summary>
        /// Gets the XML document content to create a notification.
        /// </summary>
        /// <returns>The XML document.</returns>
        XmlDocument GetXmlDocument();
    }
}
