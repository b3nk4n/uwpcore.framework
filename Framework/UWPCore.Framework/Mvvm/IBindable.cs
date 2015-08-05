using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UWPCore.Framework.Mvvm
{
    public interface IBindable : INotifyPropertyChanged
    {
        void RaisePropertyChanged([CallerMemberName]string propertyName = null);
        void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null);
    }
}
