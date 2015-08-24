using System.Collections.Generic;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The type of an input element.
    /// </summary>
    public enum InputType
    {
        Text,
        Selection
    }

    /// <summary>
    /// Class for an adaptive input element.
    /// </summary>
    public class AdaptiveInput : IAdaptiveActionsChild
    {
        /// <summary>
        /// Gets or sets the required ID that is for developers to retrieve user inputs once
        /// the app is activated (in the foreground or background).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the input type that is used to specify a text input or input from a list of pre-defined selections.
        /// On mobile and desktop, this is to specify whether you want a textbox input or a listbox input.
        /// </summary>
        public InputType Type { get; set; } = InputType.Text;

        /// <summary>
        /// Gets or sets the optional titlethat is for developers to specify a title for the input for shells to render when there is affordance.
        /// For mobile and desktop, this title will be displayed above the input.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the optional placeholderContent attribute that is the grey-out hint text for text input type.
        /// This attribute is ignored when the input type is not <see cref="InputType.Text"/>.
        /// </summary>
        public string PlaceHolderContent { get; set; }

        /// <summary>
        /// Gets or sets the optional defaultInput attribute that allows developer to provide a default input value.
        /// If the input type is <see cref="InputType.Text"/>, this will be treated as a string input;
        /// If the input type is <see cref="InputType.Selection"/>, this is expected to be the id of the available selections inside this input elements.
        /// </summary>
        public string DefaultInput { get; set; }

        /// <summary>
        /// Gets or sets the selection children. Which are only used when <see cref="Type"/> is set to <see cref="InputType.Selection"/>.
        /// </summary>
        public IList<AdaptiveSelection> Selections { get; set; } = new List<AdaptiveSelection>();

        public XElement GetXElement()
        {
            var element = new XElement("input", new XAttribute("id", Id), new XAttribute("type", Type.ToString().FirstLetterToLower()));

            if (!string.IsNullOrEmpty(Title))
            {
                element.Add(new XAttribute("title", Title));
            }

            if (!string.IsNullOrEmpty(PlaceHolderContent))
            {
                element.Add(new XAttribute("placeHolderContent", PlaceHolderContent));
            }

            if (!string.IsNullOrEmpty(DefaultInput))
            {
                element.Add(new XAttribute("defaultInput", DefaultInput));
            }

            if (Type == InputType.Selection)
            {
                foreach (var selection in Selections)
                {
                    element.Add(selection.GetXElement());
                }
            }

            return element;
        }
    }
}
