using Ninject;
using System;
using System.Threading.Tasks;
using UWPCore.Framework.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Audio
{
    /// <summary>
    /// A simple audio service to play audio files using an embedded <see cref="MediaElement"/>.
    /// </summary>
    public class AudioService : IAudioService
    {
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
        [Inject]
        public AudioService(ILocalStorageService storageService)
        {
            _storageService = storageService;
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
