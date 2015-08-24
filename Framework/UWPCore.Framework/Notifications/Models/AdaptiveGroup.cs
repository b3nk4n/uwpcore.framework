using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Class for adaptive group elements.
    /// </summary>
    public class AdaptiveGroup : IAdaptiveVisualChild
    {
        /// <summary>
        /// Gets or sets the sub groups.
        /// </summary>
        public IList<AdaptiveSubGroup> SubGroups { get; set; } = new List<AdaptiveSubGroup>();

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
