
namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Interface for services that access the display functionality.
    /// </summary>
    public interface IDisplayService
    {
        /// <summary>
        /// Requests the display to stay active.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        bool RequestActive();

        /// <summary>
        /// Releases the display to stay active, so the lockscreen can be auto-activated again.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        bool RequestRelease();

        /// <summary>
        /// Releases all requests to the display to stay active, so the lockscreen can be auto-activated again.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        bool RequestReleaseAll();
    }
}
