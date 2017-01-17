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

        public Color? StatusBarForeground { get; private set; }

        public Color? StatusBarBackground { get; private set; }

        public Color? AppShellHamburgerForeground { get; private set; }

        public Color? AppShellHamburgerBackground { get; private set; }

        public bool IsAutoConfigured
        {
            get { return false; }
        }

        /// <summary>
        /// Creates an AppColorProperties instance.
        /// </summary>
        /// <param name="theme">The main theme color.</param>
        public AppColorProperties(Color? theme)
            : this(theme, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Creates an AppColorProperties instance.
        /// </summary>
        /// <param name="theme">the main theme color.</param>
        /// <param name="titleBarForeground">The foreground of the title bar.</param>
        /// <param name="titleBarBackground">The background of the title bar.</param>
        /// <param name="statusBarForeground">The foreground of the status bar.</param>
        /// <param name="statusBarBackground">The background of the status bar.</param>
        /// <param name="appShellHamburgerForeground">
        /// The foreground of the app shell hamburger button.
        /// Remarks: When either Light or Dark theme define such a style, the other theme should define one as well. 
        ///          Otherwise theme changing issues will occure.
        /// </param>
        /// <param name="appShellHamburgerBackground">The background of the app shell hamburger button.</param>
        public AppColorProperties(Color? theme, Color? titleBarForeground, Color? titleBarBackground, Color? statusBarForeground, Color? statusBarBackground,
                                  Color? appShellHamburgerForeground, Color? appShellHamburgerBackground)
        {
            Theme = theme;
            TitleBarForeground = titleBarForeground;
            TitleBarBackground = titleBarBackground;
            StatusBarForeground = statusBarForeground;
            StatusBarBackground = statusBarBackground;
            AppShellHamburgerForeground = appShellHamburgerForeground;
            AppShellHamburgerBackground = appShellHamburgerBackground;
        }
    }
}
