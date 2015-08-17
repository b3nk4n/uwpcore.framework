namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The launch arguments.
    /// </summary>
    public interface ILaunchArgs
    {
        string Arguments { get; }

        string TileId { get; }

        bool IsValid { get; }
    }
}
