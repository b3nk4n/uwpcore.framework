using System;
using UWPCore.Framework.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Audio
{
    /// <summary>
    /// A simple audio service to play audio files using an embedded <see cref="MediaElement"/>.
    /// </summary>
    /// <remarks>
    /// Because an embedded <see cref="Windows.UI.Xaml.Controls.MediaElement"/> is used, each page gets its own instance.
    /// This implementation only works when the root element of the page is a <see cref="Page"/> 
    /// that supports multiple child elements. This audio service registers an invisible media element to the visual tree.
    /// </remarks>
    class AudioService : IAudioService
    {
        /// <summary>
        /// The visual tree name of the nested <see cref="MediaElement"/> control.
        /// </summary>
        public const string VISUAL_TREE_NAME = "__mediaElement";

        /// <summary>
        /// The media element to play audio.
        /// </summary>
        /// <remarks>
        /// The instance has to be registered to the visual tree.
        /// </remarks>
        private MediaElement _mediaElement = new MediaElement();

        public void Register(Page page)
        {
            // media element initial setup
            _mediaElement.Name = VISUAL_TREE_NAME;
            _mediaElement.Visibility = Visibility.Collapsed;

            // register media element to visual tree
            var pageContainer = page.Content as Panel;
            if (pageContainer != null && pageContainer.FindName(VISUAL_TREE_NAME) == null)
            {
                pageContainer.Children.Add(_mediaElement);
            }
        }

        public void Play(string path)
        {
            _mediaElement.Source = new Uri(string.Format("{0}/{1}", IOConstants.APPX_SCHEME, path));
            _mediaElement.Play();
        }

        public void Stop()
        {
            _mediaElement.Stop();
        }
    }

}
