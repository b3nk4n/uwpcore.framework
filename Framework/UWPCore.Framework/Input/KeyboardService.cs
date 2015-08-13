using System;

namespace UWPCore.Framework.Input
{
    /// <summary>
    /// The keyboard service for simple keyboard access.
    /// </summary>
    public class KeyboardService
    {
        /// <summary>
        /// The keyboard controller engine.
        /// </summary>
        KeyboardController _keyboardController;

        /// <summary>
        /// Creates a KeyboardService instance.
        /// </summary>
        public KeyboardService()
        {
            _keyboardController = new KeyboardController();
            _keyboardController.GoBackGestured = () => { AfterBackGesture?.Invoke(); };
            _keyboardController.GoForwardGestured = () => { AfterForwardGesture?.Invoke(); };
        }

        /// <summary>
        /// Gets or sets the AfterBack gesture.
        /// </summary>
        public Action AfterBackGesture { get; set; }

        /// <summary>
        /// Gets or sets the AfterForward gesture.
        /// </summary>
        public Action AfterForwardGesture { get; set; }
    }
}
