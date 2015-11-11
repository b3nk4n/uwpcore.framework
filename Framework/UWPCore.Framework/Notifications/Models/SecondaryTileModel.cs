using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// Model class for secondary tile.
    /// Implemented to be able to use a seperate mandatory tileId parameter with no redundancy.
    /// </summary>
    public class SecondaryTileModel
    {
        /// <summary>
        /// Gets or sets the anchor element.
        /// </summary>
        public FrameworkElement AnchorElement { get; set; }

        /// <summary>
        /// Gets or sets the popup placement.
        /// </summary>
        public Placement RequestPlacement { get; set; }

        /// <summary>
        /// Gets or sets the optional arguments.
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the phonetic name used for alphabtic ordering.
        /// </summary>
        public string PhoneticName { get; set; }

        /// <summary>
        /// Gets or sets whether the lock screen displayes badge and tile text or not.
        /// </summary>
        public bool? LockScreenDisplayBadgeAndTileText { get; set; }

        /// <summary>
        /// Gets or sets the lock screen badge logo URI.
        /// </summary>
        public Uri LockScreenBadgeLogo { get; set; }

        /// <summary>
        /// Gets the visual elements.
        /// </summary>
        public TileVisualElements VisualElements { get; private set; } = new TileVisualElements();

        /// <summary>
        /// Class for the tiles visual elements.
        /// </summary>
        public class TileVisualElements
        {
            /// <summary>
            /// Gets or sets the background color.
            /// </summary>
            public Color? BackgroundColor { get; set; }

            /// <summary>
            /// Gets or sets the foreground text.
            /// </summary>
            public ForegroundText? ForegroundText { get; set; }

            /// <summary>
            /// Gets or sets whether the name is shown on the medium tile.
            /// </summary>
            public bool? ShowNameOnSquare150x150Logo { get; set; }

            /// <summary>
            /// Gets or sets whether the name is shown on the large tile.
            /// </summary>
            public bool? ShowNameOnSquare310x310Logo { get; set; }

            /// <summary>
            /// Gets or sets whether the name is shown on the wide tile.
            /// </summary>
            public bool? ShowNameOnWide310x150Logo { get; set; }

            /// <summary>
            /// Gets or sets the medium tile image URI.
            /// </summary>
            public Uri Square150x150Logo { get; set; }

            /// <summary>
            /// Gets or sets the large tile image URI.
            /// </summary>
            public Uri Square310x310Logo { get; set; }

            /// <summary>
            /// Gets or sets the wide tile image URI.
            /// </summary>
            public Uri Wide310x150Logo { get; set; }
        }

        /// <summary>
        /// Gets the rectangle region for the flyout.
        /// </summary>
        /// <returns>The flyout region.</returns>
        public Rect Rect()
        {
            if (AnchorElement == null)
                return new Rect();

            var transform = AnchorElement.TransformToVisual(null);
            var point = transform.TransformPoint(new Point());
            var size = new Size(AnchorElement.ActualWidth, AnchorElement.ActualHeight);
            return new Rect(point, size);
        }
    }
}
