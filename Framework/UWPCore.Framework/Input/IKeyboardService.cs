using System;

namespace UWPCore.Framework.Input
{
    public interface IKeyboardService
    {
        /// <summary>
        /// Gets or sets the Back gesture.
        /// </summary>
        Action AfterBackGesture { get; set; }

        /// <summary>
        /// Gets or sets the Forward gesture.
        /// </summary>
        Action AfterForwardGesture { get; set; }

        /// <summary>
        /// Gets or sets the ControlE gesture.
        /// </summary>
        Action AfterControlEGesture { get; set; }

        /// <summary>
        /// Gets or sets the WindowsZ gesture.
        /// </summary>
        Action AfterWindowZGesture { get; set; }

        /// <summary>
        /// Register the key down event for the current page
        /// </summary>
        void RegisterForKeyDown(Action<KeyboardEventArgs> action);

        /// <summary>
        /// Unregisters the key down event. Must be called in OnNavigatedFrom
        /// </summary>
        void UnregisterForKeyDown();
    }
}
