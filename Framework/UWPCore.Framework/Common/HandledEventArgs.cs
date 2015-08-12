using System;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The class for handled event arguments.
    /// </summary>
    public class HandledEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the handled flag.
        /// </summary>
        public bool Handled { get; set; }
    }
}
