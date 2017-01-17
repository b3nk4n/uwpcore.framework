using UWPCore.Framework.Common;
using Windows.UI;
using Windows.UI.Xaml;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// The auto configured app colors.
    /// </summary>
    public class AutoAppColorProperties : IAppColorProperties
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
            get { return true; }
        }

        /// <summary>
        /// Creates auto-generated app colors depending on the dark/light theme.
        /// </summary>
        public AutoAppColorProperties()
        {
            Theme = (Color)UniversalApp.Current.Resources["SystemAccentColor"];
            if (UniversalApp.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                TitleBarForeground = Colors.White;
                TitleBarBackground = Colors.Black;
                StatusBarForeground = Colors.White;
                StatusBarBackground = Colors.Black;

                AppShellHamburgerForeground = null;
                AppShellHamburgerBackground = Colors.Transparent;
            }
            else
            {
                TitleBarForeground = Colors.Black;
                TitleBarBackground = Colors.White;
                StatusBarForeground = Colors.Black;
                StatusBarBackground = Colors.White;

                AppShellHamburgerForeground = null;
                AppShellHamburgerBackground = Colors.Transparent;
            }
        }
    }
}
