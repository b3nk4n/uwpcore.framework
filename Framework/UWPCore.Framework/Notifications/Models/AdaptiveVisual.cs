using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Class for adaptive visual elements.
    /// </summary>
    public class AdaptiveVisual : AdaptiveVisualBindingBase, IAdaptive
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the bindings child elements.
        /// </summary>
        public IList<AdaptiveBinding> Bindings { get; set; } = new List<AdaptiveBinding>();

        public XElement GetXElement()
        {
            var element = new XElement("visual", GetXAttributes());
            if (!string.IsNullOrWhiteSpace(Version))
            {
                element.Add(new XAttribute("version", Version));
            }

            foreach (var binding in Bindings)
            {
                element.Add(binding.GetXElement());
            }

            return element;
        }
    }
}
