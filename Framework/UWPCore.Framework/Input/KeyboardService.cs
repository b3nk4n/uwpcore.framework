using System;

namespace UWPCore.Framework.Input
{
    /// <summary>
    /// The keyboard service for simple keyboard access.
    /// </summary>
    public class KeyboardService
    {
        /// <summary>
        /// The keyboard helper.
        /// </summary>
        KeyboardHelper _helper;

        /// <summary>
        /// Creates a KeyboardService instance.
        /// </summary>
        public KeyboardService()
        {
            _helper = new KeyboardHelper();
            _helper.GoBackGestured = () => { AfterBackGesture?.Invoke(); };
            _helper.GoForwardGestured = () => { AfterForwardGesture?.Invoke(); };
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
