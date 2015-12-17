using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace UWPCore.Framework.Input
{
    /// <summary>
    /// The virtual key class.
    /// </summary>
    enum VKeyClass_EnUs
    {
        Control, // 0-31, 33-47, 91-95, 144-165
        Character, // 32, 48-90
        NumPad, // 96-111
        Function // 112 - 135
    }

    /// <summary>
    /// The virtual key character classes.
    /// </summary>
    public enum VKeyCharacterClass
    {
        Space,
        Numeric,
        Alphabetic
    }

    /// <summary>
    /// The keyboard helper class.
    /// </summary>
    public class KeyboardController
    {
        /// <summary>
        /// Creates a KeyboardHelper instance.
        /// </summary>
        public KeyboardController()
        {
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += CoreDispatcher_AcceleratorKeyActivated;
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
        }

        /// <summary>
        /// Cleans up the controller and unregisters all events.
        /// </summary>
        public void Cleanup()
        {
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -= CoreDispatcher_AcceleratorKeyActivated;
            Window.Current.CoreWindow.PointerPressed -= CoreWindow_PointerPressed;
        }

        /// <summary>
        /// Is fired when a key combination is pressed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args.</param>
        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            if (e.EventType.ToString().Contains("Down") && !e.Handled)
            {
                var alt = (Window.Current.CoreWindow.GetKeyState(VirtualKey.Menu) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
                var shift = (Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
                var control = (Window.Current.CoreWindow.GetKeyState(VirtualKey.Control) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
                var windows = ((Window.Current.CoreWindow.GetKeyState(VirtualKey.LeftWindows) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down) || ((Window.Current.CoreWindow.GetKeyState(VirtualKey.RightWindows) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down);
                var character = ToChar(e.VirtualKey, shift);

                var keyDown = new KeyboardEventArgs
                {
                    AltKey = alt,
                    Character = character,
                    ControlKey = control,
                    EventArgs = e,
                    ShiftKey = shift,
                    VirtualKey = e.VirtualKey
                };

                try { KeyDown?.Invoke(keyDown); }
                finally
                {
                    e.Handled = keyDown.Handled;
                }
            }
        }

        /// <summary>
        /// Gets or sets the key down action.
        /// </summary>
        public Action<KeyboardEventArgs> KeyDown { get; set; }

        /// <summary>
        /// Invoked on every mouse click, touch screen tap, or equivalent interaction when this
        /// page is active and occupies the entire window.  Used to detect browser-style next and
        /// previous mouse button clicks to navigate between pages.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            var properties = e.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed)
                return;

            // If back or foward are pressed (but not both) navigate appropriately
            bool backPressed = properties.IsXButton1Pressed;
            bool forwardPressed = properties.IsXButton2Pressed;
            if (backPressed ^ forwardPressed)
            {
                e.Handled = true;
                if (backPressed) RaiseGoBackGestured();
                if (forwardPressed) RaiseGoForwardGestured();
            }
        }

        /// <summary>
        /// Gets or sets the go forward gesture action.
        /// </summary>
        public Action GoForwardGestured { get; set; }

        /// <summary>
        /// Raises the go forward gesture action.
        /// </summary>
        protected void RaiseGoForwardGestured()
        {
            try { GoForwardGestured?.Invoke(); }
            catch { }
        }

        /// <summary>
        /// Gets or sets the go back gesture action.
        /// </summary>
        public Action GoBackGestured { get; set; }

        /// <summary>
        /// Raises the go back gesture action.
        /// </summary>
        protected void RaiseGoBackGestured()
        {
            try { GoBackGestured?.Invoke(); }
            catch { }
        }

        /// <summary>
        /// Gets or sets the CTRL-E gesture action.
        /// </summary>
        public Action ControlEGestured { get; set; }

        /// <summary>
        /// Raises the CTRL-E gesture action.
        /// </summary>
        protected void RaiseControlEGestured()
        {
            try { ControlEGestured?.Invoke(); }
            catch { }
        }

        /// <summary>
        /// Converts the key combination to a character.
        /// </summary>
        /// <param name="key">The virtual key.</param>
        /// <param name="shift">Indicates whether the shift key was pressed.</param>
        /// <returns>The converted character.</returns>
        private static char? ToChar(VirtualKey key, bool shift)
        {
            // convert virtual key to char
            if (32 == (int)key)
                return ' ';

            VirtualKey search;

            // look for simple letter
            foreach (var letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                if (Enum.TryParse<VirtualKey>(letter.ToString(), out search) && search.Equals(key))
                    return (shift) ? letter : letter.ToString().ToLower()[0];
            }

            // look for simple number
            foreach (var number in "1234567890")
            {
                if (Enum.TryParse<VirtualKey>("Number" + number.ToString(), out search) && search.Equals(key))
                    return number;
            }

            // not found
            return null;
        }
    }
}
