using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The actions system command types.
    /// </summary>
    public enum ActionsHintSystemCommand
    {
        SnoozeAndDismiss
    }

    /// <summary>
    /// Class for an adaptive actions element.
    /// </summary>
    public class AdaptiveActions : IAdaptive
    {
        /// <summary>
        /// Action: A developer can specify up to 3 custom or system actions inside a toast notification.
        /// Input: A developer can enable users to provide input to an app via a toast notification.
        /// For example, being able to type a reply to an instant message directly inside a toast.
        /// </summary>
        public IList<IAdaptiveActionsChild> Children { get; set; } = new List<IAdaptiveActionsChild>();

        /// <summary>
        /// This hint attribute will construct a dropbox for selecting snooze interval, a snooze action, and a dismiss action.
        /// The snooze and dismiss will all be handled by system (the app will not be activated when the user takes these actions).
        /// The string displayed for the input and the 2 actions will be properly localized.
        /// </summary>
        public ActionsHintSystemCommand? HintSystemCommand;

        public XElement GetXElement()
        {
            var element = new XElement("actions");

            if (HintSystemCommand.HasValue)
            {
                element.Add(new XAttribute("hint-systemCommand", HintSystemCommand.Value.ToString()));
            }

            foreach (var child in Children)
            {
                element.Add(child.GetXElement());
            }

            return element;
        }
    }
}
