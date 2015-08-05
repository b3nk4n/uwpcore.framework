
namespace UWPCore.Framework.Storage
{
    /// <summary>
    /// A collection of common IO constants.
    /// </summary>
    public static class IOConstants
    {
        /// <summary>
        /// The ms-appx scheme for app package resources.
        /// </summary>
        public const string APPX_SCHEME = "ms-appx://";

        /// <summary>
        /// The ms-appx-web scheme for web resources.
        /// </summary>
        public const string APPX_WEB_SCHEME = "ms-appx-web://";

        /// <summary>
        /// The ms-appdata scheme for local app data.
        /// </summary>
        public const string APPDATA_SCHEME = "ms-appdata://";

        /// <summary>
        /// The local folder of ms-appdata scheme for local app data.
        /// </summary>
        public const string APPDATA_LOCAL_SCHEME = APPDATA_SCHEME + "/local";

        /// <summary>
        /// The ms-resource scheme for app resources.
        /// </summary>
        public const string RESOURCE_SCHEME = "ms-resource://";
    }

}
