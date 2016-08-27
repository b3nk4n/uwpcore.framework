using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The image placement type.
    /// </summary>
    public enum ImagePlacement
    {
        Inline,
        Background,
        Peek,
        AppLogoOverride, // Toast only!
        Hero
    }

    /// <summary>
    /// The image align type.
    /// </summary>
    public enum ImageHintAlign
    {
        Stretch,
        Left,
        Center,
        Right
    }

    /// <summary>
    /// The image crop type.
    /// </summary>
    public enum ImageHintCrop
    {
        None,
        Circle
    }

    /// <summary>
    /// Class for adaptive image elements.
    /// </summary>
    public class AdaptiveImage : IAdaptiveVisualChild, IAdaptiveSubGroupChild
    {
        /// <summary>
        /// Gets or sets the image source, which can be a local or a web source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the optional image placement.
        /// </summary>
        public ImagePlacement? Placement { get; set; }

        /// <summary>
        /// Gets or sets the optional flag whether to add image query.
        /// </summary>
        public bool? AddImageQuery { get; set; }

        /// <summary>
        /// Gets or sets the image crop hint.
        /// </summary>
        public ImageHintCrop? HintCrop { get; set; }

        /// <summary>
        /// Sets or gets the optional image alt info text.
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Gets or sets the optional flag whether to remove the image margin or not.
        /// </summary>
        public bool? HintRemoveMargin { get; set; }

        /// <summary>
        /// Gets or sets the optional image alignment.
        /// </summary>
        public ImageHintAlign? HintAlign { get; set; }

        private int? _hintOverlay;
        /// <summary>
        /// Gets or sets the overlay number in range [0-100]. A value of >99 is displayed as 99+.
        /// </summary>  
        /// <remarks>
        /// In 1511, we allow you to specify an overlay for your peek image, just like your background image.
        /// Specify hint-overlay on the peek image element as an integer from 0-100.
        /// The default overlay for peek images is 0 (no overlay).
        /// </remarks>
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

        public XElement GetXElement()
        {
            var element = new XElement("image", new XAttribute("src", Source));
            if (Placement.HasValue)
            {
                element.Add(new XAttribute("placement", Placement.Value.ToString().FirstLetterToLower()));
            }
            if (!string.IsNullOrWhiteSpace(Alt))
            {
                element.Add(new XAttribute("alt", Alt));
            }
            if (AddImageQuery.HasValue)
            {
                element.Add(new XAttribute("addImageQuery", AddImageQuery.Value));
            }
            if (HintCrop.HasValue)
            {
                element.Add(new XAttribute("hint-crop", HintCrop.Value.ToString().FirstLetterToLower()));
            }
            if (HintRemoveMargin.HasValue)
            {
                element.Add(new XAttribute("hint-removeMargin", HintRemoveMargin.Value));
            }
            if (HintAlign.HasValue)
            {
                element.Add(new XAttribute("hint-align", HintAlign.Value.ToString().FirstLetterToLower()));
            }
            if (HintOverlay.HasValue)
            {
                element.Add(new XAttribute("hint-overlay", HintOverlay.Value));
            }

            return element;
        }
    }
}
