using System.Xml;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    public enum ToastScenario
    {
        Default,
        Alarm,
        Reminder,
        IncomingCall
    }

    public enum ToastDuration
    {
        Short,
        Long
    }

    public enum ToastActivationType
    {
        Foreground,
        Background,
        Protocol
    }

    /// <summary>
    /// Model class for an adaptive toast notification.
    /// </summary>
    /// <remarks>
    /// Template schema documented under <see cref="http://blogs.msdn.com/b/tiles_and_toasts/archive/2015/07/02/adaptive-and-interactive-toast-notifications-for-windows-10.aspx"/>.
    /// </remarks>
    public class AdaptiveToast : IAdaptive
    {
        public AdaptiveVisual Visual { get; set; }

        public AdaptiveActions Actions { get; set; }

        /// <summary>
        /// Gets or sets the optional launch arguments.
        /// A string that is passed to the application when it is activated by the toast.Depending on the value of activationType,
        /// this value can be received by the app in the foreground, inside the background task, or by another app that”s protocol
        /// launched from the original app. The format and contents of this string are defined by the app for its own use. 
        /// When the user taps or clicks the toast to launch its associated app, the launch string provides the context to the app
        /// that allows it to show the user a view relevant to the toast content, rather than launching in its default way.
        /// </summary>
        public string Launch { get; set; }

        /// <summary>
        /// Gets or sets the display duration, where <see cref="ToastDuration.Short"/> is the default value.
        /// This is only here for specific scenarios and appCompat, you do NOT need this for the alarm scenario anymore.
        /// Other than Office who may potentially use this for popping a toast to finish saving files, no one else should be using 
        /// this attribute.If you believe you have a real scenario for this, please reach out to us.
        /// </summary>
        public ToastDuration? Duration { get; set; }

        /// <summary>
        /// Gets or sets the app activation type, where <see cref="ToastActivationType.Foreground"/> is the default value.
        /// </summary>
        public ToastActivationType ActivationType { get; set; }

        /// <summary>
        /// Gets or sets the toast scenario, where <see cref="ToastScenario.Default"/> is the default value.
        /// You do not need this unless your scenario is to pop an alarm, reminder, or incoming call.Do not use this just for 
        /// keeping your notification persistent on screen.
        /// </summary>
        public ToastScenario? Scenario { get; set; }

        public XElement GetXElement()
        {
            var element = new XElement("toast", new XAttribute("version", "3"), Visual.GetXElement(), Actions.GetXElement());
            if (Scenario.HasValue)
            {
                element.Add(new XAttribute("scenario", Scenario.ToString().FirstLetterToLower()));
            }
            return element;
        }

        public XmlDocument GetXmlDocument()
        {
            XDocument document = new XDocument(GetXElement());
            document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            var xmlDoc = new XmlDocument();
            var xmlAsString = document.ToString();
            xmlDoc.LoadXml(document.ToString());
            return xmlDoc;
        }
    }
}
