using System;

namespace UWPCore.Framework.Common
{
    public class LaunchArgs : ILaunchArgs
    {
        public string Arguments { get; private set; }

        public string TileId { get; private set; }

        public bool IsValid { get; private set; } = false;

        public LaunchArgs () { }

        public LaunchArgs (string args, string tileId)
        {
            Arguments = args;
            TileId = tileId;
            IsValid = true;
        }
    }
}
