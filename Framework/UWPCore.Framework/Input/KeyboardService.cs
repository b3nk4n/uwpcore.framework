using System;

namespace UWPCore.Framework.Input
{
    /// <summary>
    /// The keyboard service for simple keyboard access.
    /// </summary>
    public class KeyboardService : IKeyboardService
    {
        /// <summary>
        /// The keyboard controller engine.
        /// </summary>
        private KeyboardController _keyboardController;

        /// <summary>
        /// Creates a KeyboardService instance.
        /// </summary>
        public KeyboardService()
        {
            _keyboardController = new KeyboardController();

            _keyboardController.KeyDown = (e) =>
            {
                e.Handled = true;

                // use this to hide and show the menu
                if (e.OnlyWindows && e.Character.ToString().ToLower().Equals("z"))
                    AfterWindowZGesture?.Invoke();

                // use this to place focus in search box
                else if (e.OnlyControl && e.Character.ToString().ToLower().Equals("e"))
                    AfterControlEGesture?.Invoke();

                // use this to nav back
                else if (e.VirtualKey == Windows.System.VirtualKey.GoBack)
                    AfterBackGesture?.Invoke();
                else if (e.VirtualKey == Windows.System.VirtualKey.NavigationLeft)
                    AfterBackGesture?.Invoke();
                else if (e.VirtualKey == Windows.System.VirtualKey.GamepadLeftShoulder)
                    AfterBackGesture?.Invoke();
                else if (e.OnlyAlt && e.VirtualKey == Windows.System.VirtualKey.Back)
                    AfterBackGesture?.Invoke();
                else if (e.OnlyAlt && e.VirtualKey == Windows.System.VirtualKey.Left)
                    AfterBackGesture?.Invoke();

                // use this to nav forward
                else if (e.VirtualKey == Windows.System.VirtualKey.GoForward)
                    AfterForwardGesture?.Invoke();
                else if (e.VirtualKey == Windows.System.VirtualKey.NavigationRight)
                    AfterForwardGesture?.Invoke();
                else if (e.VirtualKey == Windows.System.VirtualKey.GamepadRightShoulder)
                    AfterForwardGesture?.Invoke();
                else if (e.OnlyAlt && e.VirtualKey == Windows.System.VirtualKey.Right)
                    AfterForwardGesture?.Invoke();
                else
                    e.Handled = false;

                // call the general callback where pages can subsribe for.
                AfterKeyDown?.Invoke(e);
            };

            _keyboardController.GoBackGestured = () => { AfterBackGesture?.Invoke(); };
            _keyboardController.GoForwardGestured = () => { AfterForwardGesture?.Invoke(); };
        }

        /// <summary>
        /// Gets or sets the Back gesture.
        /// </summary>
        public Action AfterBackGesture { get; set; }

        /// <summary>
        /// Gets or sets the Forward gesture.
        /// </summary>
        public Action AfterForwardGesture { get; set; }

        /// <summary>
        /// Gets or sets the ControlE gesture.
        /// </summary>
        public Action AfterControlEGesture { get; set; }

        /// <summary>
        /// Gets or sets the WindowsZ gesture.
        /// </summary>
        public Action AfterWindowZGesture { get; set; }

        /// <summary>
        /// Gets or sets the general key combination gesture.
        /// </summary>
        private Action<KeyboardEventArgs> AfterKeyDown { get; set; }

        public void RegisterForKeyDown(Action<KeyboardEventArgs> action)
        {
            AfterKeyDown = action;
        }

        public void UnregisterForKeyDown()
        {
            AfterKeyDown = null;
        }
    }
}
