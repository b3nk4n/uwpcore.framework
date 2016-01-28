using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// Class to simplify and shorten the string locallization.
    /// </summary>
    /// <remarks>
    /// Important remarks:
    /// - for x:Uid of a different library, use "/<LibraryName>/Resources/<Key>"
    /// - to make the app realize it supports multiple languages, create dummy .resw files
    ///   locally for each language with one dummy data that it is really generated.
    /// </remarks>
    public sealed class Localizer
    {
        /// <summary>
        /// The resource loader.
        /// </summary>
        public ResourceLoader _resourceLoader;

        /// <summary>
        /// Creates a Localizer instance.
        /// </summary>
        /// <param name="assemblyName">The optional assembly name, in case the resource files are located in a different one.</param>
        public Localizer(string assemblyName = null)
        {
            // do not init in design mode, which causes error with preview data when used in view model.
            if (!DesignMode.DesignModeEnabled)
            {
                if (assemblyName == null)
                    _resourceLoader = ResourceLoader.GetForCurrentView();
                else
                    _resourceLoader = ResourceLoader.GetForViewIndependentUse(assemblyName + "/Resources");
            }
        }

        /// <summary>
        /// Gets the string resource.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <returns>Returns the string resource.</returns>
        public string Get(string resource)
        {
            // allow dot-style resource keys as defined in the resw. files
            var resourceKey = resource.Replace('.', '/');

            return _resourceLoader.GetString(resourceKey);
        }

        /// <summary>
        /// Gets the string resource.
        /// </summary>
        /// <param name="uri">The resource URI.</param>
        /// <returns>Returns the string resource.</returns>
        public string GetForUri(Uri uri)
        {
            return _resourceLoader.GetStringForUri(uri);
        }
    }
}
