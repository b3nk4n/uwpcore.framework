using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    /// <summary>
    /// The navigation service that manages and simplifies the page navigation.
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// The current page type state key.
        /// </summary>
        public const string CURRENT_PAGE_TYPE_KEY = "CurrentPageType";

        /// <summary>
        /// The current page param state key.
        /// </summary>
        public const string CURRENT_PAGE_PARAM_KEY = "CurrentPageParam";

        /// <summary>
        /// The navigate state key.
        /// </summary>
        public const string NAVIGATE_STATE_KEY = "NavigateState";

        /// <summary>
        /// Gets the wrapped frame.
        /// </summary>
        public FrameFacade FrameFacade { get; private set; }

        /// <summary>
        /// The last navigation parameter.
        /// </summary>
        private object _lastNavigationParameter;

        /// <summary>
        /// The last navigation type.
        /// </summary>
        private string _lastNavigationType;

        /// <summary>
        /// Creates a NavigationService instance.
        /// </summary>
        /// <param name="frame">The page frame.</param>
        public NavigationService(Frame frame)
        {
            FrameFacade = new FrameFacade(frame);
            FrameFacade.Navigating += async (s, e) =>
            {
                if (e.Suspending)
                    return;

                // allow the viewmodel to cancel navigation
                e.Cancel = !NavigatingFrom(false);
                if (!e.Cancel)
                {
                    await NavigateFromAsync(false);
                }
            };
            FrameFacade.Navigated += (s, e) =>
            {
                NavigateTo(e.NavigationMode, e.Parameter);
            };
        }

        /// <summary>
        /// Navigating from before navigation (cancellable).
        /// </summary>
        /// <param name="suspending">The suspending flag.</param>
        /// <returns>Returns True when navigating from is ok, else False when to cancel.</returns>
        bool NavigatingFrom(bool suspending)
        {
            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    var args = new NavigatingEventArgs
                    {
                        PageType = FrameFacade.CurrentPageType,
                        Parameter = FrameFacade.CurrentPageParam,
                        Suspending = suspending,
                    };
                    dataContext.OnNavigatingFrom(args);
                    return !args.Cancel;
                }
            }
            return true;
        }

        /// <summary>
        /// Navigate from that is called after navigation.
        /// </summary>
        /// <param name="suspending">The suspending flag.</param>
        private async Task NavigateFromAsync(bool suspending)
        {
            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                // call viewmodel
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    dataContext.Identifier = string.Format("Page-{0}", FrameFacade.BackStackDepth);
                    var pageState = FrameFacade.GetPageStateContainer(page.GetType());
                    await dataContext.OnNavigatedFromAsync(pageState, suspending);
                }
            }
        }

        /// <summary>
        /// Navigates to the current page type defined in <see cref="CurrentPageType"/>.
        /// </summary>
        /// <param name="mode">The navigation mode.</param>
        /// <param name="parameter">The parameter.</param>
        private void NavigateTo(NavigationMode mode, string parameter)
        {
            _lastNavigationParameter = parameter;
            _lastNavigationType = FrameFacade.Content.GetType().FullName;

            // clears the frame state when a page is newly navigated
            /*if (mode == NavigationMode.New) // FIXME: is it to much to clear state frame every NEW-navigation. Remember: because there is no FrameID (up to now), we have
            {
                //FrameFacade.ClearPageState((FrameFacade.Content as Page).GetType()); // only this page state, not all page states (which caused error sometimes for the second navigateTo:New, but load state method was called properly)
                //FrameFacade.ClearFrameState(); // original Template 10
            }*/
            // --> let ViewModel OnNavigateTo descide whether to load the state or not (because the method gets a NavigationMode parameter!) 

            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                // call viewmodel
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    if (dataContext.Identifier != null
                        && (mode == NavigationMode.Forward || mode == NavigationMode.Back))
                    {
                        // don't call load if cached && navigating back/forward
                        return;
                    }
                    else
                    {
                        // prepare for state load
                        dataContext.NavigationService = this;
                        var pageState = FrameFacade.GetPageStateContainer(page.GetType());
                        dataContext.OnNavigatedTo(parameter, mode, pageState);
                    }
                }
             }
        }

        /// <summary>
        /// Open the page in a new window instaad of navigating to an exisitng frame.
        /// </summary>
        /// <param name="page">The page type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="size">The prefered windows size.</param>
        /// <returns>The view ID of the new window.</returns>
        public void OpenAsync(Type page, string parameter = null, ViewSizePreference size = ViewSizePreference.UseHalf)
        {
            // FIXME: this will spawn a new window instead of navigating to an existing frame.
            // --> not supported up to now. Have a second look at Template10 until they finised it!
            throw new NotImplementedException();

            //var coreView = CoreApplication.CreateNewView();
            //ApplicationView view = null;
            //var create = new Action(() =>
            //{
            //    // setup content
            //    var frame = new Frame();
            //    frame.NavigationFailed += (s, e) => { Debugger.Break(); };
            //    frame.Navigate(page, parameter);

            //    // create window
            //    var window = Window.Current;
            //    window.Content = frame;

            //    // setup view/collapse
            //    view = ApplicationView.GetForCurrentView();
            //    Windows.Foundation.TypedEventHandler<ApplicationView, ApplicationViewConsolidatedEventArgs> consolidated = null;
            //    consolidated = new Windows.Foundation.TypedEventHandler<ApplicationView, ApplicationViewConsolidatedEventArgs>((s, e) =>
            //    {
            //        (s as ApplicationView).Consolidated -= consolidated;
            //        if (CoreApplication.GetCurrentView().IsMain)
            //            return;
            //        try { window.Close(); }
            //        finally { CoreApplication.GetCurrentView().CoreWindow.Activate(); }
            //    });
            //    view.Consolidated += consolidated;
            //});

            //// execute create
            //await WindowWrapper.Current().Dispatcher.DispatchAsync(create);

            //// show view
            //if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(view.Id, size))
            //{
            //    // change focus
            //    await ApplicationViewSwitcher.SwitchAsync(view.Id);
            //}
            //return view.Id;
        }

        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <param name="page">The page type.</param>
        /// <param name="parameter">The navigation parameter.</param>
        /// <returns>Returns True for success, else False.</returns>
        public bool Navigate(Type page, object parameter = null)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            if (page.FullName.Equals(_lastNavigationType)
                && parameter == _lastNavigationParameter)
                return false;
            return FrameFacade.Navigate(page, parameter);
        }

        /// <summary>
        /// Save the navigation state.
        /// </summary>
        public void SaveNavigationState()
        {
            var state = FrameFacade.GetPageStateContainer(GetType());
            state[CURRENT_PAGE_TYPE_KEY] = CurrentPageType.ToString();
            state[CURRENT_PAGE_PARAM_KEY] = CurrentPageParam;
            state[NAVIGATE_STATE_KEY] = FrameFacade.GetNavigationState();
        }

        /// <summary>
        /// Restores the saved navigation state.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        public bool RestoreSavedNavigationState()
        {
            try
            {
                var state = FrameFacade.GetPageStateContainer(GetType());
                string currentPageType = state[CURRENT_PAGE_TYPE_KEY].ToString();
                Type pageTypeOfAppAssembly = Type.GetType(currentPageType + ", " + UniversalApp.AppAssemblyName);

                FrameFacade.CurrentPageType = pageTypeOfAppAssembly;
                FrameFacade.CurrentPageParam = state[CURRENT_PAGE_PARAM_KEY]?.ToString();
                FrameFacade.SetNavigationState(state[NAVIGATE_STATE_KEY].ToString());
                NavigateTo(NavigationMode.Refresh, FrameFacade.CurrentPageParam);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Refreshes the current frame.
        /// </summary>
        public void Refresh()
        {
            FrameFacade.Refresh();
        }

        /// <summary>
        /// Goes back when possible.
        /// </summary>
        public void GoBack()
        {
            if (FrameFacade.CanGoBack)
                FrameFacade.GoBack();
        }

        /// <summary>
        /// Gets whether we can go back in the history stack.
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return FrameFacade.CanGoBack;
            }
        }

        /// <summary>
        /// Goes forward when possible.
        /// </summary>
        public void GoForward()
        {
            if (FrameFacade.CanGoForward)
                FrameFacade.GoForward();
        }

        /// <summary>
        /// Gets whether we can go forward in the history stack.
        /// </summary>
        public bool CanGoForward
        {
            get
            {
                return FrameFacade.CanGoForward;
            }
        }

        /// <summary>
        /// Clears the history stack.
        /// </summary>
        public void ClearHistory()
        {
            FrameFacade.Frame.BackStack.Clear();
        }

        public void Resuming(){ } // FIXME: not referenced and empty. Delete? Check again after some more progress in Template10!

        /// <summary>
        /// When suspending, it saves the navigation state and fires the navigate from event.
        /// </summary>
        public async Task SuspendingAsync()
        {
            SaveNavigationState();
            await NavigateFromAsync(true);
        }

        /// <summary>
        /// Shaws the settings flyout.
        /// </summary>
        /// <param name="flyout">The flyout.</param>
        /// <param name="parameter">The parameter.</param>
        public void Show(SettingsFlyout flyout, string parameter = null)
        {
            if (flyout == null)
                throw new ArgumentNullException(nameof(flyout));
            var dataContext = flyout.DataContext as INavigable;
            if (dataContext != null)
            {
                dataContext.OnNavigatedTo(parameter, NavigationMode.New, null);
            }
            flyout.Show();
        }

        /// <summary>
        /// Gets the current page type.
        /// </summary>
        public Type CurrentPageType
        {
            get
            {
                return FrameFacade.CurrentPageType;
            }
        }

        /// <summary>
        /// Get the current page parameter.
        /// </summary>
        public string CurrentPageParam
        {
            get
            {
                return FrameFacade.CurrentPageParam;
            }
        }
    }
}
