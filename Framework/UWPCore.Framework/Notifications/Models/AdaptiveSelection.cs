using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Class for an adaptive selection element.
    /// </summary>
    public class AdaptiveSelection : IAdaptive
    {
        /// <summary>
        /// Gets or sets the required ID attribute and is for apps to retrieve back the user selected input after the app is activated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the required content. It provides the string to display for this selection element.
        /// </summary>
        public string Content { get; set; }

        public XElement GetXElement()
        {
            return new XElement("selection", new XAttribute("id", Id), new XAttribute("content", Content));
        }
    }
}
