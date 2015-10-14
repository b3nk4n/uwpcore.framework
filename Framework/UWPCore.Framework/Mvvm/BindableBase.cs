using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// An implementation of <see cref="IBindable"/> interface to simplify 
    /// model and view models of the MVVM pattern.
    /// </summary> 
    [DataContract]
    public abstract class BindableBase : IBindable
    {
        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public async void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;

            await WindowWrapper.Current().Dispatcher.DispatchAsync(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
 
        public bool Set<T>(ref T storage, T value, [CallerMemberName()]string propertyName = null)
        {
            if (Equals(storage, value))
                return false;
            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
