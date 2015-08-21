using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    public enum SubGroupTextStacking
    {
        Top,
        Center,
        Bottom
    }

    public class AdaptiveSubGroup : IAdaptiveVisualChild
    {
        private int? _hintWeight;
        /// <summary>
        /// none|logo|name|nameAndLogo
        /// </summary>
        public int? HintWeight
        {
            get
            {
                return _hintWeight;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    _hintWeight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("HintWeight", "Must in range of [0-100]");
                }
            }
        }
        public SubGroupTextStacking? HintTextStacking { get; set; }

        public List<IAdaptiveSubGroupChild> Children { get; set; }
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
