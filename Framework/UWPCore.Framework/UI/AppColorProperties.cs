using System;
using Windows.UI;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// The manual color properties of the app.
    /// </summary>
    public class AppColorProperties : IAppColorProperties
    {
        public Color? Theme { get; private set; }

        public Color? TitleBarForeground { get; private set; }

        public Color? TitleBarBackground { get; private set; }

        public bool IsAutoConfigured
        {
            get { return false; }
        }

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
