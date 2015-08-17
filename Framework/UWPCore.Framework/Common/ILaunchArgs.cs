namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The launch arguments interface.
    /// </summary>
    public interface ILaunchArgs
    {
        /// <summary>
        /// Gets the launch arguments.
        /// </summary>
        string Arguments { get; }

        /// <summary>
        /// Gets the tile id.
        /// </summary>
        string TileId { get; }

        /// <summary>
        /// Gets whether the lauch argument is valid, that means it is not empty.
        /// </summary>
        bool IsValid { get; }
    }
}
