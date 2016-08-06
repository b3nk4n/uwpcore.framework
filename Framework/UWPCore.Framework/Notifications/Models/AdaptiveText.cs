using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// the text alignment type.
    /// </summary>
    public enum TextHintAlign
    {
        Left,
        Center,
        Right
    }

    /// <summary>
    /// The text style type.
    /// </summary>
    /// <remarks>
    /// caption   12 epx Regular (DEFAULT)
    /// body      15 epx Regular
    /// base      15 epx SemiBold
    /// subtitle  20 epx Regular
    /// title	  24 epx Semilight
    /// subheader 34 epx Light
    /// header    46 epx Light
    /// </remarks>
    public enum TextStyle
    {
        Caption,
        CaptionSubtle,
        Body,
        BodySubtle,
        Base,
        BaseSubtle,
        Subtitle,
        SubtitleSubtle,
        Title,
        TitleSubtle,
        TitleNumeral,
        Subheader,
        SubheaderSubtle,
        SubHeaderNumeral,
        Header,
        HeaderSubtle,
        HeaderNumber
    }

    /// <summary>
    /// The placement of the text.
    /// </summary>
    public enum TextPlacement
    {
        Attribution // Toast only
    }

    /// <summary>
    /// Class for adaptive text elements.
    /// </summary>
    public class AdaptiveText : IAdaptiveVisualChild, IAdaptiveSubGroupChild
    {
        /// <summary>
        /// Gets or sets the required content to display.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the optional flag whether to wrap the text or not.
        /// </summary>
        public bool? HintWrap { get; set; }

        /// <summary>
        /// Gets or sets the optional maximum number of lines.
        /// </summary>
        public int? HintMaxLines { get; set; }

        /// <summary>
        /// Gets or sets the optional minimum number of lines.
        /// </summary>
        public int? HintMinLines { get; set; }

        /// <summary>
        /// Gets or sets the optional text style.
        /// </summary>
        public TextStyle? HintStyle { get; set; }

        /// <summary>
        /// Gets or sets the optional text alignment.
        /// </summary>
        public TextHintAlign? HintAlign { get; set; }

        /// <summary>
        /// Gets or sets the optional text placement.
        /// </summary>
        public TextPlacement? Placement { get; set; }

        public XElement GetXElement()
        {
            var element = new XElement("text", Content);

            if (Placement.HasValue)
            {
                element.Add(new XAttribute("placement", Placement.Value.ToString().FirstLetterToLower()));
            }
            if (HintWrap.HasValue)
            {
                element.Add(new XAttribute("hint-wrap", HintWrap.Value));
            }
            if (HintMaxLines.HasValue)
            {
                element.Add(new XAttribute("hint-maxLines", HintMaxLines.Value));
            }
            if (HintMinLines.HasValue)
            {
                element.Add(new XAttribute("hint-wrap", HintMinLines.Value));
            }
            if (HintAlign.HasValue)
            {
                element.Add(new XAttribute("hint-align", HintAlign.Value.ToString().FirstLetterToLower()));
            }
            if (HintStyle.HasValue)
            {
                element.Add(new XAttribute("hint-style", HintStyle.Value.ToString().FirstLetterToLower()));
            }

            return element;
        }
    }
}
