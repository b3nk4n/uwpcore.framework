using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Class for an adaptive audio element.
    /// </summary>
    public class AdaptiveAudio : IAdaptive
    {
        public const string NOTIFICATION_DEFAULT = "ms-winsoundevent:Notification.Default";
        public const string NOTIFICATION_IM = "ms-winsoundevent:Notification.IM";
        public const string NOTIFICATION_MAIL = "ms-winsoundevent:Notification.Mail";
        public const string NOTIFICATION_REMINDER = "ms-winsoundevent:Notification.Reminder";
        public const string NOTIFICATION_SMS = "ms-winsoundevent:Notification.SMS";
        public const string NOTIFICATION_LOOPING_ALARM = "ms-winsoundevent:Notification.Looping.Alarm";
        public const string NOTIFICATION_LOOPING_ALARM2 = "ms-winsoundevent:Notification.Looping.Alarm2";
        public const string NOTIFICATION_LOOPING_ALARM3 = "ms-winsoundevent:Notification.Looping.Alarm3";
        public const string NOTIFICATION_LOOPING_ALARM4 = "ms-winsoundevent:Notification.Looping.Alarm4";
        public const string NOTIFICATION_LOOPING_ALARM5 = "ms-winsoundevent:Notification.Looping.Alarm5";
        public const string NOTIFICATION_LOOPING_ALARM6 = "ms-winsoundevent:Notification.Looping.Alarm6";
        public const string NOTIFICATION_LOOPING_ALARM7 = "ms-winsoundevent:Notification.Looping.Alarm7";
        public const string NOTIFICATION_LOOPING_ALARM8 = "ms-winsoundevent:Notification.Looping.Alarm8";
        public const string NOTIFICATION_LOOPING_ALARM9 = "ms-winsoundevent:Notification.Looping.Alarm9";
        public const string NOTIFICATION_LOOPING_ALARM10 = "ms-winsoundevent:Notification.Looping.Alarm10";
        public const string NOTIFICATION_LOOPING_CALL = "ms-winsoundevent:Notification.Looping.Call";
        public const string NOTIFICATION_LOOPING_CALL2 = "ms-winsoundevent:Notification.Looping.Call2";
        public const string NOTIFICATION_LOOPING_CALL3 = "ms-winsoundevent:Notification.Looping.Call3";
        public const string NOTIFICATION_LOOPING_CALL4 = "ms-winsoundevent:Notification.Looping.Call4";
        public const string NOTIFICATION_LOOPING_CALL5 = "ms-winsoundevent:Notification.Looping.Call5";
        public const string NOTIFICATION_LOOPING_CALL6 = "ms-winsoundevent:Notification.Looping.Call6";
        public const string NOTIFICATION_LOOPING_CALL7 = "ms-winsoundevent:Notification.Looping.Call7";
        public const string NOTIFICATION_LOOPING_CALL8 = "ms-winsoundevent:Notification.Looping.Call8";
        public const string NOTIFICATION_LOOPING_CALL9 = "ms-winsoundevent:Notification.Looping.Call9";
        public const string NOTIFICATION_LOOPING_CALL10 = "ms-winsoundevent:Notification.Looping.Call10";

        /// <summary>
        /// Sets or gets whether the audio file is looped.
        /// Set to true if the sound should repeat as long as the toast is shown; false to play only once.
        /// If this attribute is set to true, the duration attribute in the toast element must also be set.
        /// There are specific sounds provided to be used when looping. Note that neither looping audio nor
        /// long-duration toasts are not supported on Windows Phone 8.1.
        /// </summary>
        public bool? Loop { get; set; }

        /// <summary>
        /// Sets or gets whether the audio file is played or muted.
        /// True to mute the sound; false to allow the toast notification sound to play.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Gets or sets the source of the audio file.
        /// The media file to play in place of the default sound. On Windows, this attribute can have one
        /// of the following string values, such as:
        /// AdaptiveAudio.DEFAULT
        /// AdaptiveAudio.IM
        /// ...
        /// 
        /// On Windows Phone 8.1 (and WM10?), this attribute can also contain the path to a local audio file,
        /// with one of the following prefixes:
        /// ms-appx:///
        /// ms-appdata:///
        /// </summary>
        public string Source { get; set; } 

        public XElement GetXElement()
        {
            var element = new XElement("audio");
            if (!string.IsNullOrEmpty(Source))
            {
                element.Add(new XAttribute("src", Source));
            }
            if (Loop.HasValue)
            {
                element.Add(new XAttribute("loop", Loop.Value.ToString().FirstLetterToLower()));
            }
            if (Silent.HasValue)
            {
                element.Add(new XAttribute("silent", Silent.Value.ToString().FirstLetterToLower()));
            }
            return element;
        }
    }
}