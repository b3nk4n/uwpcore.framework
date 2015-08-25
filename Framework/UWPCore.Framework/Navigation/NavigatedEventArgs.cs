using System;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    /// <summary>
    /// The navigated event arguments class.
    /// </summary>
    public class NavigatedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a NavigatedEventArgs instance.
        /// </summary>
        public NavigatedEventArgs() { }

        /// <summary>
        /// Creates a NavigatedEventArgs instance.
        /// </summary>
        /// <param name="e">The navigation event args.</param>
        public NavigatedEventArgs(NavigationEventArgs e)
        {
            PageType = e.SourcePageType;
            Parameter = e.Parameter?.ToString();
            NavigationMode = e.NavigationMode;
        }
        /// <summary>
        /// Gets or sets the navigation mode.
        /// </summary>
        public NavigationMode NavigationMode { get; set; }

        /// <summary>
        /// Gets or sets the page type.
        /// </summary>
        public Type PageType { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        public object Parameter { get; set; }
    }
}
