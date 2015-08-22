using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    public class AdaptiveVisual : AdaptiveVisualBindingBase, IAdaptive
    {
        public string Version { get; set; }

        public List<AdaptiveBinding> Bindings
        {
            get; set;
        }
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
