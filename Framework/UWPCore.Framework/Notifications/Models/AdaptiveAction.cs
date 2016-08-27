using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    public enum ActionPlacement
    {
        ContextMenu // Toast only
    }

    /// <summary>
    /// Class for an adaptive action element.
    /// </summary>
    public class AdaptiveAction : IAdaptiveActionsChild
    {
        /// <summary>
        /// Gets or sets the text string displayed on the button. The content attribute is required.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the required arguments attribute that describes the app-defined data that the app can
        /// later retrieve once it is activated from user taking this action.
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Gets or sets the optional activationType attribute where <see cref="ToastActivationType.Foreground"/> is default value.
        /// It describes what kind of activation this action will cause – foreground, background, or launching another app via protocol launch.
        /// </summary>
        public ToastActivationType? ActivationType { get; set; }

        /// <summary>
        /// Gets or sets the optional imageUri that is used to provide an image icon for this action
        /// to display inside the button alone with the text content.
        /// Attention: Is requried, when <see cref="HintInputId"/> is used.
        /// </summary>
        public string ImageUri { get; set; }

        /// <summary>
        /// This is specifically used for the quick reply scenario.
        /// The value needs to be the id of the input element desired to be associated with.
        /// In mobile and desktop, this will put the button right next to the input box.
        /// Attention: Requires to set the <see cref="ImageUri"/> property.
        /// </summary>
        public string HintInputId { get; set; }

        /// <summary>
        /// Gets or sets the placement of the action.
        /// </summary>
        public ActionPlacement? Placement { get; set; }

        public XElement GetXElement()
        {
            var element = new XElement("action", new XAttribute("content", Content), new XAttribute("arguments", Arguments));

            if (ActivationType.HasValue)
            {
                element.Add(new XAttribute("activationType", ActivationType.Value.ToString().FirstLetterToLower()));
            }
            if (Placement.HasValue)
            {
                element.Add(new XAttribute("placement", Placement.Value.ToString().FirstLetterToLower()));
            }

            if (!string.IsNullOrEmpty(ImageUri))
            {
                element.Add(new XAttribute("imageUri", ImageUri));
            }

            if (!string.IsNullOrEmpty(HintInputId))
            {
                element.Add(new XAttribute("hint-inputId", HintInputId));
            }

            return element;
        }
    }
}
