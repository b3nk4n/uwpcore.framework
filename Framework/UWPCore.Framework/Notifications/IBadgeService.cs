using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The badge service interface to update the badge icon on the live tile and lock screen.
    /// </summary>
    public interface IBadgeService
    {
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

        /// <summary>
        /// Gets the badge factory.
        /// </summary>
        IBadgeFactory Factory { get; }
    }
}
