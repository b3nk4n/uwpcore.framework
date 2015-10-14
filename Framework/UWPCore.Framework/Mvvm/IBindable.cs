using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// Interface based on <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public interface IBindable : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        void RaisePropertyChanged([CallerMemberName]string propertyName = null);

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null);
    }
}
