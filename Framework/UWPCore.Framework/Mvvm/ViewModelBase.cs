using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.IoC;
using UWPCore.Framework.Navigation;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Mvvm
{
    /// <summary>
    /// The base class of view model in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        /// <summary>
        /// Gets the injector.
        /// </summary>
        public IInjector Injector
        {
            get
            {
                if (IsDesignMode)
                    return new DesignTimeInjector();
                
                return IoC.Injector.Instance;
            }
        }

        /// <summary>
        /// Gets or sets the navigation service.
        /// </summary>
        public NavigationService NavigationService { get; set; }

        public DispatcherWrapper Dispatcher { get { return WindowWrapper.Current(NavigationService)?.Dispatcher; } }

        public StateItems SessionState { get { return UniversalApp.Current.SessionState; } }

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated to.
        /// NOTE: This method is called BEFORE OnNavigatedTo() of the next page, but after OnNavigatingTo()!
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="mode">The navigation mode.</param>
        /// <param name="state">The state.</param>
        public virtual void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state) { }

        /// <summary>
        /// Hooh method that is called when a page using this view model is navigating from.
        /// </summary>
        /// <param name="args">The navigating event args.</param>
        public virtual void OnNavigatingFrom(NavigatingEventArgs args) { }

        /// <summary>
        /// Hook method that is called when a page using this view model was navigated from.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="suspending">The suspending indicator.</param>
        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending) { return Task.FromResult<object>(null); }

        /// <summary>
        /// Hook method that is called when the page is suspended.
        /// Remember that <see cref="OnNavigatedFromAsync(IDictionary{string, object}, bool)"/> is not called in this case.
        /// </summary>
        /// <param name="op">The suspension args.</param>
        public virtual Task OnSuspendingAsync(SuspendingOperation op) { return Task.FromResult<object>(null); }

        /// <summary>
        /// Gets called when the view model gets resumed.
        /// </summary>
        public virtual void OnResume() { }

        /// <summary>
        /// Gets whether we are in design mode.
        /// </summary>
        public bool IsDesignMode
        {
            get
            {
                return DesignMode.DesignModeEnabled;
            }
        }
    }
}
