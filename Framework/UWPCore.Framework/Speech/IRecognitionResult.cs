using System;
using System.Collections.Generic;

namespace UWPCore.Framework.Speech
{
    public interface IRecognitionResult
    {
        /// <summary>
        /// Gets the spoken text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Gets the duration of the spoken text.
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets the interpretation of the given property phrase name.
        /// </summary>
        /// <param name="property">The property phrase name.</param>
        /// <returns>Returns the interpretation or NULL.</returns>
        IDictionary<string, string> Interpretations { get; }
    }
}
