using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Audio
{
    /// <summary>
    /// The audio service interace to play some audio files.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="path">The path to the audio file, such as 'Assets/sound.wav'.</param>
        Task PlayAsync(string path);

        /// <summary>
        /// Plays an audio stream.
        /// </summary>
        /// <param name="stream">The stream to play.</param>
        void PlayFromStream(IRandomAccessStreamWithContentType stream);

        /// <summary>
        /// Stops to play the current audio file.
        /// </summary>
        void Stop();
    }

}
