using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The badge glyph icons where not all of them are supported in Windows Phone.
    /// See <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/hh779719.aspx"/> for more information.
    /// </summary>
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
    /// The badge service interface to update the badge icon on the live tile and lock screen.
    /// </summary>
    public interface IBadgeService
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

        /// <summary>
        /// Gets the application badge updater.
        /// </summary>
        /// <param name="applicationId">The application ID that is required for background tasks.</param>
        /// <returns>Returns the badge updater.</returns>
        BadgeUpdater GetBadgeUpdaterForApplication(string applicationId = null);

        /// <summary>
        /// Gets the secondary tile badge updater.
        /// </summary>
        /// <param name="tileId">The tile ID.</param>
        /// <returns>Returns the badge updater.</returns>
        BadgeUpdater GetBadgeUpdaterForSecondaryTile(string tileId);
    }
}
