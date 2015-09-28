using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Navigation;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);
            Logger.WriteLine("VIEWMODEL - OnNavigatedTo");
        }

        public override void OnNavigatingFrom(NavigatingEventArgs args)
        {
            base.OnNavigatingFrom(args);
            Logger.WriteLine("VIEWMODEL - OnNavigatingFrom");
        }

        public async override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            await base.OnNavigatedFromAsync(state, suspending);
            Logger.WriteLine("VIEWMODEL - OnNavigatedFromAsync");
        }
    }
}
