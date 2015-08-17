using System;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The launch arguments.
    /// </summary>
    public class LaunchArgs : ILaunchArgs
    {
        public string Arguments { get; private set; }

        public string TileId { get; private set; }

        public bool IsValid { get; private set; } = false;

        /// <summary>
        /// Creates a invalid LaunchArgs instance.
        /// </summary>
        public LaunchArgs () { }

        /// <summary>
        /// Creates a LaunchArgs instance.
        /// </summary>
        /// <param name="args">The launch arguments.</param>
        /// <param name="tileId">The tile ID.</param>
        public LaunchArgs (string args, string tileId)
        {
            Arguments = args;
            TileId = tileId;
            IsValid = true;
        }
    }
}
