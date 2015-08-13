using System;
using Windows.System;
using Windows.UI.Core;

namespace UWPCore.Framework.Input
{
    /// <summary>
    /// The keyboard event arguments.
    /// </summary>
    public class KeyboardEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets whether the ALT key was pressed.
        /// </summary>
        public bool AltKey { get; set; }

        /// <summary>
        /// Gets or sets whether the CTRL key was pressed.
        /// </summary>
        public bool ControlKey { get; set; }

        /// <summary>
        /// Gets or sets whether the SHIFT key was pressed.
        /// </summary>
        public bool ShiftKey { get; set; }

        /// <summary>
        /// Gets or sets the pressed virtual key.
        /// </summary>
        public VirtualKey VirtualKey { get; set; }

        /// <summary>
        /// Gets or sets the accelerator event arguments.
        /// </summary>
        public AcceleratorKeyEventArgs EventArgs { get; set; }

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        public char? Character { get; set; }
    }
}
