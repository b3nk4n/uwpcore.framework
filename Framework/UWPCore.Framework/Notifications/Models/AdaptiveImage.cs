using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    public enum ImagePlacement
    {
        Inline,
        Background,
        Peek
    }

    public enum ImageHintAlign
    {
        Stretch,
        Left,
        Center,
        Right
    }

    public class AdaptiveImage : IAdaptiveVisualChild, IAdaptiveSubGroupChild
    {
        public string Source { get; set; }
        public ImagePlacement? Placement { get; set; }
        public bool? AddImageQuery { get; set; }

        public enum ImageHintCrop
        {
            None,
            Circle
        }
        public ImageHintCrop? HintCrop { get; set; }
        public string Alt { get; set; }
        public bool? HintRemoveMargin { get; set; }
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
