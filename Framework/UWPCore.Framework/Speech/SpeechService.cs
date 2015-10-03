using System;
using System.Threading.Tasks;
using UWPCore.Framework.Audio;
using UWPCore.Framework.Launcher;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Storage;
using UWPCore.Framework.UI;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;

namespace UWPCore.Framework.Speech
{
    /// <summary>
    /// The speech service class for voice commands, speech recognition and speech synthesization.
    /// </summary>
    /// <remarks>
    /// Speech recognition requires: CAP_MICROPHONE
    /// </remarks>
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

        /// <summary>
        /// The dialog service.
        /// </summary>
        private IDialogService _dialogService;

        /// <summary>
        /// The storage service.
        /// </summary>
        private IStorageService _storageService;

        /// <summary>
        /// Gets the lazily created synthesizer instance.
        /// </summary>
        public SpeechSynthesizer Synthesizer
        {
            get
            {
                if (_snythesizer == null)
                    _snythesizer = new SpeechSynthesizer();
                return _snythesizer;
            }
        }
        private SpeechSynthesizer _snythesizer;

        /// <summary>
        /// Gets the lazily created recognizer instance.
        /// </summary>
        /// <remarks>
        /// Created lazily to be able to use this serivce without Microphone capability.
        /// </remarks>
        public SpeechRecognizer Recognizer
        {
            get
            {
                if (_recognizer == null)
                    _recognizer = new SpeechRecognizer();
                return _recognizer;
            }
        }
        private SpeechRecognizer _recognizer;

        /// <summary>
        /// Creates a SpeechService instance.
        /// </summary>
        public SpeechService()
        {
            _audioService = new AudioService();
            _dialogService = new DialogService();
            _storageService = new LocalStorageService();
        }

        #region Voice Commands

        public async Task InstallCommandSets(string packageFilePath)
        {
            //if (!_hasInstalledCommands)
            {
                try
                {
                    var storageFile = await _storageService.GetFileFromApplicationAsync(packageFilePath);
                    await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile as StorageFile);

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

        // see: https://msdn.microsoft.com/en-us/library/dn630427.aspx
        public async Task<IRecognitionResult> RecoginizeUI()
        {
            try
            {
                //Recognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
                //Recognizer.UIOptions.ExampleText = @"Ex. 'weather for London'";

                //var webSearchGrammar = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.WebSearch, "webSearch");
                //Recognizer.Constraints.Add(webSearchGrammar);

                // Compile the dictation grammar by default.
                await Recognizer.CompileConstraintsAsync(); // TODO: FIXME: is this call required? Each time?

                // Start recognition.
                SpeechRecognitionResult speechRecognitionResult = await Recognizer.RecognizeWithUIAsync();
                return new RecognitionResult(speechRecognitionResult);
            }
            catch (Exception exception)
            {
                // Handle the speech privacy policy error.
                if ((uint)exception.HResult == HRESULT_PRIVACY_STATEMENT_DECLINED)
                {
                    await SettingsLauncher.LaunchPrivacyAccountsAsync();
                }
                else
                {
                    await _dialogService.ShowAsync(exception.Message, "Exception");
                }
            }
            return null;
        }

        #endregion
    }
}
