using System;
using System.Collections.Generic;
using Windows.Media.SpeechRecognition;

namespace UWPCore.Framework.Speech
{
    /// <summary>
    /// The the recognition result.
    /// </summary>
    public class RecognitionResult : IRecognitionResult
    {
        /// <summary>
        /// Gets the recognized text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// Gets the recognition duration.
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Gets the most likely interpretations.
        /// </summary>
        public IDictionary<string, string> Interpretations { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Creates a RecognitionResult instance.
        /// </summary>
        /// <param name="result">The speech result to wrap.</param>
        public RecognitionResult(SpeechRecognitionResult result)
        {
            Text = result.Text;
            Duration = result.PhraseDuration;

            if (result.RulePath != null && result.RulePath.Count > 0)
                CommandName = result.RulePath[0];

            if (result.SemanticInterpretation != null)
            {
                // copy only the most likely value
                foreach (var key in result.SemanticInterpretation.Properties.Keys)
                {
                    Interpretations.Add(key, result.SemanticInterpretation.Properties[key][0]);
                }
            }
        }
    }
}
