using System;
using System.Collections.Generic;
using Windows.Media.SpeechRecognition;

namespace UWPCore.Framework.Speech
{
    public class RecognitionResult : IRecognitionResult
    {
        public string Text { get; private set; }

        public string CommandName { get; private set; }

        public TimeSpan Duration { get; private set; }

        public IDictionary<string, string> Interpretations { get; private set; } = new Dictionary<string, string>();

        public RecognitionResult(SpeechRecognitionResult result)
        {
            CommandName = result.RulePath[0];
            Text = result.Text;
            Duration = result.PhraseDuration;

            // copy only the most likely value
            foreach (var key in result.SemanticInterpretation.Properties.Keys)
            {
                Interpretations.Add(key, result.SemanticInterpretation.Properties[key][0]);
            }
        }
    }
}
