using Windows.UI;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The color properties of the app.
    /// </summary>
    public class AppColorProperties
    {
        /// <summary>
        /// Gets the main theme color.
        /// </summary>
        public Color? Theme { get; private set; }

        /// <summary>
        /// Gets the title bar foreground color.
        /// </summary>
        public Color? TitleBarForeground { get; private set; }

        /// <summary>
        /// Gets the title bar background color.
        /// </summary>
        public Color? TitleBarBackground { get; private set; }

        /// <summary>
        /// Creates an AppColorProperties instance.
        /// </summary>
        /// <param name="theme">The main theme color.</param>
        public AppColorProperties(Color? theme)
            : this(theme, null, null)
        {
        }

        /// <summary>
        /// Creates an AppColorProperties instance.
        /// </summary>
        /// <param name="theme">the main theme color.</param>
        /// <param name="titleBarForeground">The foreground of the title bar.</param>
        /// <param name="titleBarBackground">The background of the title bar.</param>
        public AppColorProperties(Color? theme, Color? titleBarForeground, Color? titleBarBackground)
        {
            Theme = theme;
            TitleBarForeground = titleBarForeground;
            TitleBarBackground = titleBarBackground;
        }
    }
}
