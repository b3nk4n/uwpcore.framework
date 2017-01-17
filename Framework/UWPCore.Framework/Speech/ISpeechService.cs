using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;

namespace UWPCore.Framework.Speech
{
    /// <summary>
    /// The speech service interface.
    /// </summary>
    public interface ISpeechService
    {
        /// <summary>
        /// Gets the speech synthesizer.
        /// </summary>
        SpeechSynthesizer Synthesizer { get; }

        /// <summary>
        /// Gets the speech recognizer.
        /// </summary>
        SpeechRecognizer Recognizer { get; }

        /// <summary>
        /// Installs void commands from a Command Definition (VCD) XML file.
        /// </summary>
        /// <param name="packageFilePath">The path to the voice commands file in the application package.</param>
        void InstallCommandSets(string packageFilePath);

        /// <summary>
        /// Gets the voice commands from start up arguments.
        /// </summary>
        /// <param name="args">The activated event args.</param>
        /// <returns>Returns the recognized voice command or NULL.</returns>
        IRecognitionResult GetVoiceCommand(IActivatedEventArgs args);

        /// <summary>
        /// Speaks a text which code is secured with a try-catch block.
        /// </summary>
        /// <remarks>
        /// Use this method to ensure that the synthesazion will not cause
        /// any exceptions, e.g. HRESULT: 0x80045508.
        /// </remarks>
        /// <param name="text">The content text to speak.</param>
        Task SpeakTextAsync(string text);

        /// <summary>
        /// Stops to speak.
        /// </summary>
        void StopSpeak();

        /// <summary>
        /// Recognizes text via the UI.
        /// </summary>
        /// <returns>The recognized results.</returns>
        Task<IRecognitionResult> RecoginizeUI();
    }
}
