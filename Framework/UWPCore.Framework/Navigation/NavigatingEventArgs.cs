using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    /// <summary>
    /// The navigating event arguments class.
    /// </summary>
    public class NavigatingEventArgs : NavigatedEventArgs
    {
        /// <summary>
        /// Creates a NavigatingEventArgs instance.
        /// </summary>
        public NavigatingEventArgs() { }

        /// <summary>
        /// Creates a NavigatingEventArgs instance.
        /// </summary>
        /// <param name="e">The navigating cancel event args.</param>
        public NavigatingEventArgs(NavigatingCancelEventArgs e)
        {
            NavigationMode = e.NavigationMode;
            PageType = e.SourcePageType;
            Parameter = e.Parameter?.ToString();
        }

        /// <summary>
        /// Gets or sets the cancel flag.
        /// </summary>
        public bool Cancel { get; set; } = false;

        /// <summary>
        /// Gets or sets the suspending flag.
        /// </summary>
        public bool Suspending { get; set; } = false;
    }
}
