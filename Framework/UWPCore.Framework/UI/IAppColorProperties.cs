using Windows.UI;

namespace UWPCore.Framework.UI
{
    public interface IAppColorProperties
    {
        /// <summary>
        /// Gets whether this is a auto configured or an custom color setting.
        /// </summary>
        bool IsAutoConfigured { get; }

        /// <summary>
        /// Gets the main theme color.
        /// </summary>
        Color? Theme { get; }

        /// <summary>
        /// Gets the title bar foreground color.
        /// </summary>
        Color? TitleBarForeground { get; }

        /// <summary>
        /// Gets the title bar background color.
        /// </summary>
        Color? TitleBarBackground { get; }

        /// <summary>
        /// Gets the status bar foreground color.
        /// </summary>
        Color? StatusBarForeground { get; }

        /// <summary>
        /// Gets the status bar background color.
        /// </summary>
        Color? StatusBarBackground { get; }

        /// <summary>
        /// Gets the hamburger foreground color.
        /// Remarks: When either Light or Dark theme define such a style, the other theme should define one as well. 
        ///          Otherwise theme changing issues will occure.
        /// </summary>
        Color? AppShellHamburgerForeground { get; }

        /// <summary>
        /// Gets the hamburger background color.
        /// </summary>
        Color? AppShellHamburgerBackground { get; }
    }
}
