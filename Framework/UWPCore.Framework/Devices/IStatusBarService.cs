using System.Threading.Tasks;
using Windows.UI;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service interface to access the status bar, which is only available in Windows 10 Mobile.
    /// </summary>
    public interface IStatusBarService : IDeviceService
    {
        /// <summary>
        /// Shows the status bar.
        /// </summary>
        /// <returns>The async task to wait for.</returns>
        Task ShowAsync();

        /// <summary>
        /// Hides the status bar.
        /// </summary>
        /// <returns>The async task to wait for.</returns>
        Task HideAsync();

        /// <summary>
        /// Starts the progress.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="isIntermediate">Sets whether it shows a percentage value [0,1] or an intermediate indicator.</param>
        /// <returns>The async task to wait for.</returns>
        Task StartProgressAsync(string text, bool isIntermediate = false);

        /// <summary>
        /// Updates the progress.
        /// </summary>
        /// <param name="value">The percentage value [0,1] or null for intermediate prgress.</param>
        /// <param name="text">The updated text, or NULL for no change.</param>
        void UpdateProgress(double? value, string text = null);

        /// <summary>
        /// Stops the progress.
        /// </summary>
        /// <returns>The async task to wait for.</returns>
        Task StopProgressAsync();

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        Color ForegroundColor { set; get; }

        /// <summary>
        /// Gets or sets the background color including its opacity.
        /// </summary>
        Color BackgroundColor { set; get; }
    }
}
