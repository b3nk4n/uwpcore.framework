
namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// Command interface, that allows to raise the execute changed events.
    /// </summary>
    public interface ICommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
