using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The badge glyph icons where not all of them are supported in Windows Phone.
    /// </summary>
    /// <remarks>
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh779719.aspx"/> for more information.
    /// </remarks>
    public enum BadgeGlyphIcon
    {
        None,
        Activity,
        Alarm,
        Alert,
        Attention,
        Available,
        Away,
        Busy,
        Error,
        NewMessage,
        Paused,
        Playing,
        Unavailable
    }

    /// <summary>
    /// Inferface for factories to create badge notifications based on a template.
    /// </summary>
    /// <remarks>
    /// For further information about the templates refer to <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh779719.aspx"/>.
    /// </remarks>
    public interface IBadgeFactory
    {
        /// <summary>
        /// Creates a badge notification.
        /// </summary>
        /// <param name="value">The number value.</param>
        /// <returns>Returns the badge notification.</returns>
        BadgeNotification CreateBadgeNumber(int value);

        /// <summary>
        /// Creates a badge notification.
        /// </summary>
        /// <param name="glyph">The glyph icon type.</param>
        /// <returns>Returns the badge notification.</returns>
        BadgeNotification CreateBadgeNumber(BadgeGlyphIcon glyph);
    }
}
