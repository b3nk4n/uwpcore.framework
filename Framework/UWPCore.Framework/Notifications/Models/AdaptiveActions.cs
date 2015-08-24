using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace UWPCore.Framework.Notifications.Models
{
    public enum ActionsHintSystemCommand
    {
        SnoozeAndDismiss,
        // TODO: more system commands supported?
    }

    public class AdaptiveActions : IAdaptive
    {
        /// <summary>
        /// Action: A developer can specify up to 3 custom or system actions inside a toast notification.
        /// Input: A developer can enable users to provide input to an app via a toast notification.
        /// For example, being able to type a reply to an instant message directly inside a toast.
        /// </summary>
        public List<IAdaptiveActionsChild> Children { get; set; } = new List<IAdaptiveActionsChild>();

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
