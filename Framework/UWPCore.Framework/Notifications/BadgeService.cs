using Windows.UI.Notifications;

namespace UWPCore.Framework.Notifications
{
    /// <summary>
    /// The badge service to update the badge icons on live tile or lock screen.
    /// </summary>
    public class BadgeService : IBadgeService
    {
        /// <summary>
        /// The badge factory.
        /// </summary>
        private IBadgeFactory _badgeFactory;

        /// <summary>
        /// Creates a BadgeService instance.
        /// </summary>
        public BadgeService()
        {
            _badgeFactory = new BadgeFactory();
        }

        public BadgeUpdater GetBadgeUpdaterForApplication(string applicationId = null)
        {
            return BadgeUpdateManager.CreateBadgeUpdaterForApplication();
        }

        public BadgeUpdater GetBadgeUpdaterForSecondaryTile(string tileId)
        {
            return BadgeUpdateManager.CreateBadgeUpdaterForSecondaryTile(tileId);
        }

        public IBadgeFactory Factory
        {
            get
            {
                return _badgeFactory;
            }
        }
    }
}
