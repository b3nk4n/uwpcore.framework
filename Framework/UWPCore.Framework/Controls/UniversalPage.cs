using UWPCore.Framework.Common;
using UWPCore.Framework.IoC;
using UWPCore.Framework.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The base class for every page. Provides some short access functions to reduce boilerplate code.
    /// </summary>
    /// <remarks>
    /// The base class MUST be used when using MVVM, to ensure the navigation events of <see cref="Mvvm.ViewModelBase"/> gets called.
    /// </remarks>
    public class UniversalPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // call navigation service events from here to ensure ViewModel navigation methods are called in right order
            NavigationService.NavigateTo(e.NavigationMode, e.Parameter);
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            // call navigation service events from here to ensure ViewModel navigation methods are called in right order
            e.Cancel = !NavigationService.NavigatingFrom(e, false);

            if (!e.Cancel)
            {
                await NavigationService.NavigateFromAsync(false);
            }
        }

        /// <summary>
        /// Gets the navigation serivce.
        /// </summary>
        public NavigationService NavigationService
        {
            get
            {
                return UniversalApp.Current.NavigationService;
            }
        }

        /// <summary>
        /// Gets the injector.
        /// </summary>
        public IInjector Injector
        {
            get
            {
                return Injector;
            }
        }
    }
}
