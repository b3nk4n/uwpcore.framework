using System;
using System.Diagnostics;

namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality 
    /// to other objects by invoking delegates. 
    /// The default return value for the CanExecute method is 'true'.
    /// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
    /// <see cref="CanExecute"/> is expected to return a different value.
    /// </summary>
    /// <see cref="http://codepaste.net/jgxazh"/>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// The madatory action to execute.
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// The optional function to check whether the action can be executed.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a DelegateCommand instance.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The optional execution predicate.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute ?? (() => true);
        }

        /// <summary>
        /// Determines whether this <see cref="DelegateCommand"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            try { return _canExecute(); }
            catch { return false; }
        }

        /// <summary>
        /// Executes the <see cref="DelegateCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;
            try { _execute(); }
            catch { Debugger.Break(); }
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// A command whose sole purpose is to relay its functionality 
    /// to other objects by invoking delegates. 
    /// The default return value for the CanExecute method is 'true'.
    /// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
    /// <see cref="CanExecute"/> is expected to return a different value.
    /// </summary>
    /// <typeparam name="T">the parameter type.</typeparam>
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// The madatory action to execute.
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// The optional function to check whether the action can be executed.
        /// </summary>
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a DelegateCommand instance.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The optional execution predicate.</param>
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute ?? (e => true);
        }

        /// <summary>
        /// Determines whether this <see cref="DelegateCommand"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        //[DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            try
            {
                var _Value = (T)Convert.ChangeType(parameter, typeof(T));
                return _canExecute == null ? true : _canExecute(_Value);
            }
            catch { return false; }
        }

        /// <summary>
        /// Executes the <see cref="DelegateCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;
            var _Value = (T)Convert.ChangeType(parameter, typeof(T));
            _execute(_Value);
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
