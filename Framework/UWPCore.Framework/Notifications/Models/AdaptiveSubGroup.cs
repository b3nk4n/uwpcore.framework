using System.Collections.Generic;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Class for an adaptive sub group element.
    /// </summary>
    public class AdaptiveSubGroup : IAdaptiveVisualChild
    {
        private int? _hintWeight;
        /// <summary>
        /// Gets or sets the optional hint weight in range of [0-100].
        /// </summary>
        public int? HintWeight
        {
            get
            {
                return _hintWeight;
            }

            set
            {
                if (value < 0)
                    _hintWeight = 0;
                else if (value > 100)
                    _hintWeight = 100;
                else
                    _hintWeight = value;   
            }
        }

        /// <summary>
        /// Gets or sets the optional text stacking.
        /// </summary>
        public VisualHintTextStacking? HintTextStacking { get; set; }

        /// <summary>
        /// Gets or sets the adaptive children elements.
        /// </summary>
        public IList<IAdaptiveSubGroupChild> Children { get; set; } = new List<IAdaptiveSubGroupChild>();

        public XElement GetXElement()
        {
            var element = new XElement("subgroup");
            if (HintWeight.HasValue)
            {
                element.Add(new XAttribute("hint-weight", HintWeight.Value));
            }
            if (HintTextStacking.HasValue)
            {
                element.Add(new XAttribute("hint-textStacking", HintTextStacking.Value.ToString().FirstLetterToLower()));
            }

            foreach (var child in Children)
            {
                element.Add(child.GetXElement());
            }

            return element;
        }
    }
}