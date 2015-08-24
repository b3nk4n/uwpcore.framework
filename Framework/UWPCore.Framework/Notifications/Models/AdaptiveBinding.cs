using System.Collections.Generic;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The text stacking types for visual elements.
    /// </summary>
    public enum VisualHintTextStacking
    {
        Top,
        Center,
        Bottom
    }

    /// <summary>
    /// The presentations types for visual elements.
    /// </summary>
    public enum VisualHintPresentation
    {
        Photos,
        People,
        Contact
    }

    /// <summary>
    /// Tile template name v3.
    /// </summary>
    public enum VisualTemplate
    {
        TileSmall,
        TileMedium,
        TileWide,
        TileLarge, // Desktop only!
        ToastGeneric // Toast only template!
    }

    /// <summary>
    /// Class for adaptive bindings.
    /// </summary>
    public class AdaptiveBinding : AdaptiveVisualBindingBase, IAdaptive
    {
        /// <summary>
        /// Gets or sets the tile template name v3.
        /// </summary>
        public VisualTemplate Template { get; set; }
        
        /// <summary>
        /// Gets or sets the fallback tile template name v1.
        /// </summary>
        public string Fallback { get; set; }

        /// <summary>
        /// Gets or sets the optional text stacking type.
        /// </summary>
        public VisualHintTextStacking? HintTextStacking { get; set; }

        /// <summary>
        /// Gets or sets the optional presentation type.
        /// </summary>
        public VisualHintPresentation? HintPresentation { get; set; }

        private int? _hintOverlay;
        /// <summary>
        /// Gets or sets the overlay number in range [0-100]. A value of >99 is displayed as 99+.
        /// </summary>     
        public int? HintOverlay
        {
            get
            {
                return _hintOverlay;
            }
            set
            {
                if (value < 0)
                    _hintOverlay = 0;
                else if (value > 100)
                    _hintOverlay = 100;
                else
                    _hintOverlay = value;
            }
        }

        /// <summary>
        /// Gets or sets the first lock detail status.
        /// </summary>
        public string HintLockDetailedStatus1 { get; set; }

        /// <summary>
        /// Gets or sets the second lock detail status.
        /// </summary>
        public string HintLockDetailedStatus2 { get; set; }

        /// <summary>
        /// Gets or sets the third lock detail status.
        /// </summary>
        public string HintLockDetailedStatus3 { get; set; }

        /// <summary>
        /// Gets or sets the visual children elements.
        /// </summary>
        public IList<IAdaptiveVisualChild> Children { get; set; } = new List<IAdaptiveVisualChild>();

        public XElement GetXElement()
        {
            var element = new XElement("binding", GetXAttributes());
            element.Add(new XAttribute("template", Template.ToString()));
            if (HintTextStacking.HasValue)
            {
                element.Add(new XAttribute("hint-textStacking", HintTextStacking.Value.ToString().FirstLetterToLower()));
            }

            if (HintOverlay.HasValue)
            {
                element.Add(new XAttribute("hint-overlay", HintOverlay.Value));
            }

            if (HintPresentation.HasValue)
            {
                element.Add(new XAttribute("hint-presentation", HintPresentation.Value.ToString().FirstLetterToLower()));
            }

            if (!string.IsNullOrWhiteSpace(HintLockDetailedStatus1))
            {
                element.Add(new XAttribute("hint-lockDetailedStatus1", HintLockDetailedStatus1));
            }

            if (!string.IsNullOrWhiteSpace(HintLockDetailedStatus2))
            {
                element.Add(new XAttribute("hint-lockDetailedStatus2", HintLockDetailedStatus2));
            }

            if (!string.IsNullOrWhiteSpace(HintLockDetailedStatus3))
            {
                element.Add(new XAttribute("hint-lockDetailedStatus3", HintLockDetailedStatus3));
            }

            if (!string.IsNullOrWhiteSpace(Fallback))
            {
                element.Add(new XAttribute("fallback", Fallback));
            }

            foreach (var child in Children)
            {
                element.Add(child.GetXElement());
            }

            return element;
        }
    }
}
