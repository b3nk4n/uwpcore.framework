using System;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Storage;
using Windows.Storage.Streams;
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
    public class AudioService : IAudioService
    {
        /// <summary>
        /// The visual tree name of the nested <see cref="MediaElement"/> control.
        /// </summary>
        public const string VISUAL_TREE_NAME = "__mediaElement";

        private IStorageService _storageService;

        /// <summary>
        /// The media element to play audio.
        /// </summary>
        /// <remarks>
        /// The instance has to be registered to the visual tree.
        /// </remarks>
        private MediaElement _mediaElement = new MediaElement();

        /// <summary>
        /// Creates an AudioService instance.
        /// </summary>
        public AudioService()
        {
            _storageService = new LocalStorageService();
        }

        public async Task PlayAsync(string path)
        {
            var storageFile = await _storageService.GetFileFromApplicationAsync(path);

            if (storageFile == null)
                    return;

            var stream = await storageFile.OpenReadAsync();
            PlayFromStream(stream);
        }

        public void PlayFromStream(IRandomAccessStreamWithContentType stream)
        {
            _mediaElement.SetSource(stream, stream.ContentType);
            _mediaElement.Play();
        }

        public void Stop()
        {
            _mediaElement.Stop();
        }
    }

}
