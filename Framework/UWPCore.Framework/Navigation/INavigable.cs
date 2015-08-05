using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    public interface INavigable : IBindable
    {
        void OnNavigatedTo(string parameter, NavigationMode mode, IDictionary<string, object> state);
        Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending);
        void OnNavigatingFrom(NavigatingEventArgs args);
        NavigationService NavigationService { get; set; }
        string Identifier { get; set; }
    }
}
