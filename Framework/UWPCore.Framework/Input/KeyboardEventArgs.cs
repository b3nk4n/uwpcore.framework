using System;
using Windows.System;
using Windows.UI.Core;

namespace UWPCore.Framework.Input
{
    public class KeyboardEventArgs : EventArgs
    {
        public bool AltKey { get; set; }
        public bool ControlKey { get; set; }
        public bool ShiftKey { get; set; }
        public VirtualKey VirtualKey { get; set; }
        public AcceleratorKeyEventArgs EventArgs { get; set; }
        public char? Character { get; set; }
    }
}
