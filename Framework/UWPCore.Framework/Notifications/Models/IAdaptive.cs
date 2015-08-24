using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The interface for adaptive elements.
    /// </summary>
    public interface IAdaptive
    {
        /// <summary>
        /// Gets the XML element and its content.
        /// </summary>
        /// <returns>Returns the XML element.</returns>
        XElement GetXElement();
    }
}
