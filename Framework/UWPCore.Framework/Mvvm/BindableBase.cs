using System.ComponentModel;
using System.Runtime.CompilerServices;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Mvvm
{
    public abstract class BindableBase : IBindable
    {
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

        public void Set<T>(ref T storage, T value, [CallerMemberName()]string propertyName = null)
        {
            if (object.Equals(storage, value))
                return;
            storage = value;
            RaisePropertyChanged(propertyName);
        }
    }
}
