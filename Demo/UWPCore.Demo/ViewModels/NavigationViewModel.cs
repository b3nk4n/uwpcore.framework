using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private IDialogService _dialogService;

        public NavigationViewModel()
        {
            _dialogService = Injector.Get<IDialogService>();
        }

        private async void FrameFacade_BackRequested(object sender, Framework.Common.HandledEventArgs e)
        {
            // cancel back
            e.Handled = true;

            await AskForGoingBack();
        }

        private async Task AskForGoingBack()
        {
            var res = await _dialogService.ShowAsync("Realy want to go back?", "Question", 0, 1,
                new UICommand("YES") { Id = "y" },
                new UICommand("NO") { Id = "n" });

            if (res.Id.ToString() == "y")
            {
                NavigationService.GoBack();
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);
            NavigationService.FrameFacade.BackRequested += FrameFacade_BackRequested;
            Logger.WriteLine("VIEWMODEL - OnNavigatedTo (param: " + parameter + ")");
        }

        public override async void OnNavigatingFrom(NavigatingEventArgs args)
        {
            base.OnNavigatingFrom(args);
            await Task.Delay(1000);
            Logger.WriteLine("VIEWMODEL - OnNavigatingFrom");
        }

        public async override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            await base.OnNavigatedFromAsync(state, suspending);

            if (!suspending)
                NavigationService.FrameFacade.BackRequested -= FrameFacade_BackRequested;

            Logger.WriteLine("VIEWMODEL - OnNavigatedFromAsync");
        }

        public string Parameter { get { return _parameter; } set { Set(ref _parameter, value); } }
        private string _parameter = string.Empty;
    }
}
