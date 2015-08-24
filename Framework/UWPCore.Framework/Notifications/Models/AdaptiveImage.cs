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
        AppLogoOverride // Toast only!
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

            return element;
        }
    }
}
