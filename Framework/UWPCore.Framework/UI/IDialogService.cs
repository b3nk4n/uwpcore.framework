using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// The interface for a dialog service.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Gets whether a dialog is open.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Shows a message dialog.
        /// </summary>
        /// <param name="content">The content text.</param>
        /// <param name="title">The title text.</param>
        /// <returns>Returns the message box result.</returns>
        Task ShowAsync(string content, string title);

        /// <summary>
        /// Shows a message dialog.
        /// </summary>
        /// <param name="content">The content text.</param>
        /// <param name="title">The title text.</param>
        /// <param name="enterIndex">The index of the Enter key.</param>
        /// <param name="escIndex">The index of the Esc key.</param>
        /// <param name="commands">The commands.</param>
        /// <returns>Returns the message box result.</returns>
        Task<IUICommand> ShowAsync(string content, string title, uint? enterIndex, uint? escIndex, params IUICommand[] commands);
    }
}