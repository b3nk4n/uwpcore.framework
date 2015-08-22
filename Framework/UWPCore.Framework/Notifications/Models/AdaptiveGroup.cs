using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    public class AdaptiveGroup : IAdaptiveVisualChild
    {
        public List<AdaptiveSubGroup> SubGroups
        {
            get; set;
        }
        public XElement GetXElement()
        {
            var element = new XElement("group");
            foreach (var subgroup in SubGroups)
            {
                element.Add(subgroup.GetXElement());
            }

            return element;
        }
    }
}
