using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Audio
{
    /// <summary>
    /// The audio service interace to play some audio files.
    /// </summary>
    /// <remarks>
    /// This service requries a registration to the <see cref="Page"/>, do not forget!!!
    /// </remarks>
    public interface IAudioService
    {
        /// <summary>
        /// Registers the media media element to the visual tree to be ready to be played.
        /// </summary>
        /// <param name="page">The app page.</param>
        void Register(Page page);

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="path">The path to the audio file, such as 'Assets/sound.wav'.</param>
        void Play(string path);

        /// <summary>
        /// Stops to play the current audio file.
        /// </summary>
        void Stop();
    }

}
