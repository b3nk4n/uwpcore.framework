using System;
using System.Threading.Tasks;
using UWPCore.Framework.Audio;
using UWPCore.Framework.Logging;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;

namespace UWPCore.Framework.Speech
{
    /// <summary>
    /// The speech service class for voice commands, speech recognition and speech synthesization.
    /// </summary>
    public class SpeechService : ISpeechService
    {
        /// <summary>
        /// The error result when the privacy statement was declined.
        /// </summary>
        public const uint HRESULT_PRIVACY_STATEMENT_DECLINED = 0x80045509;

        /// <summary>
        /// The audio service.
        /// </summary>
        private IAudioService _audioService;

        public SpeechSynthesizer Synthesizer { get; private set; }

        public SpeechRecognizer Recognizer { get; private set; }

        /// <summary>
        /// Creates a SpeechService instance.
        /// </summary>
        public SpeechService()
        {
            Synthesizer = new SpeechSynthesizer();
            Recognizer = new SpeechRecognizer();
            _audioService = new AudioService();
        }

        #region Voice Commands

        public async void InstallCommandSets(Uri voiceCommandSetsUri)
        {
            //if (!_hasInstalledCommands)
            {
                try
                {
                    var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(voiceCommandSetsUri); // TODO: use storage service
                    await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);

                    //_hasInstalledCommands = true;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e, "Voice Commands failed to install");
                }
            }
        }

        public IRecognitionResult GetVoiceCommand(IActivatedEventArgs args)
        {
            if (args.Kind != ActivationKind.VoiceCommand)
                return null;

            var commandArgs = args as VoiceCommandActivatedEventArgs;

            SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

            // Get the name of the voice command and the text spoken
            if (speechRecognitionResult.Status != SpeechRecognitionResultStatus.Success)
                return null;

            var voiceCommandName = speechRecognitionResult.RulePath[0];
            var textSpoken = speechRecognitionResult.Text;
            var duration = speechRecognitionResult.PhraseDuration;

            // The commandMode is either "voice" or "text", and it indicates how the voice command was entered by the user.
            // Apps should respect "text" mode by providing feedback in a silent form.
            //string commandMode = this.SemanticInterpretation("commandMode", speechRecognitionResult);

            return new RecognitionResult(speechRecognitionResult);
        }

        #endregion

        #region Synthesizer

        public async Task SpeakTextAsync(string text)
        {
            try
            {
                SpeechSynthesisStream stream = await Synthesizer.SynthesizeTextToStreamAsync(text);
                _audioService.PlayFromStream(stream);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e, "Speaking text failed");
            }
        }

        public void StopSpeak()
        {
            _audioService.Stop();
        }

        #endregion

        #region Recognizer

        #endregion
    }
}
