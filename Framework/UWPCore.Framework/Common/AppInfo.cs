using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// Facade class to get important information about the app.
    /// </summary>
    public static class AppInfo
    {
        /// <summary>
        /// Gets the app title.
        /// </summary>
        public static string AppTitle
        {
            get
            {
                return Package.Current.DisplayName;
            }
        }

        /// <summary>
        /// Gets the publisher name.
        /// </summary>
        public static string PublisherName
        {
            get
            {
                return Package.Current.PublisherDisplayName;
            }
        }

        /// <summary>
        /// Gets the store link.
        /// </summary>
        public static Uri StoreLink
        {
            get
            {
                return CurrentApp.LinkUri;
            }
        }

        /// <summary>
        /// Gets the apps product ID.
        /// </summary>
        public static string ProductId
        {
            get
            {
                return StoreLink.AbsolutePath.Replace("/store/apps/", string.Empty);
            }
        }

        /// <summary>
        /// Gets the version number as a string.
        /// </summary>
        public static string Version
        {
            get
            {
                var version = Package.Current.Id.Version;
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }
    }
}
