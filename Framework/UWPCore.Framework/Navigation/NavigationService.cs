using System;
using System.Linq;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.Logging;
using Windows.ApplicationModel;
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
        /// The dispatcher of this navigation service instance.
        /// </summary>
        public DispatcherWrapper Dispatcher { get { return WindowWrapper.Current(this).Dispatcher; } }

        /// <summary>
        /// Creates a NavigationService instance.
        /// </summary>
        /// <param name="frame">The page frame.</param>
        internal NavigationService(Frame frame)
        {
            try
            {
                FrameFacade = new FrameFacade(frame);
                FrameFacade.Navigated += (s, e) =>
                {
                    if (e.PageType == UniversalApp.Current.DefaultPage)
                    {
                        ClearHistory();
                    }

                    // KEEP THIS EVENT REGISTERED: without having this empty event registered, for whatever reason no navigation takes place
                    // Navigation method calls are moved to UniversalPage, to ensure the call order of ViewModel navigation events is aligned
                    // with the one of a Page.
                };
            }
            catch (Exception)
            {
                // exception in ShareTarget here.
            }
        }

        /// <summary>
        /// Navigating from before navigation (cancellable).
        /// </summary>
        /// <param name="suspending">The suspending flag.</param>
        /// <returns>Returns True when navigating from is ok, else False when to cancel.</returns>
        internal bool NavigatingFrom(NavigatingCancelEventArgs args, bool suspending)
        {
            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    var internalArgs = new NavigatingEventArgs()
                    {
                        PageType = args.SourcePageType,
                        Parameter = args.Parameter,
                        Suspending = suspending,
                        NavigationMode = args.NavigationMode
                    };

                    dataContext.OnNavigatingFrom(internalArgs);
                    return !internalArgs.Cancel;
                }
            }
            return true;
        }

        /// <summary>
        /// Navigate from that is called after navigation.
        /// </summary>
        /// <param name="suspending">The suspending flag.</param>
        internal async Task NavigateFromAsync(bool suspending)
        {
            var page = FrameFacade.Content as Page;

            if (page != null)
            {
                // call viewmodel
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
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
        internal void NavigateTo(NavigationMode mode, object parameter)
        {
            _lastNavigationParameter = parameter;
            _lastNavigationType = FrameFacade.Content.GetType().FullName;

            // clears the frame state when a page is newly navigated (not just refreshed or back navigated)
            if (mode == NavigationMode.New)
            {
                FrameFacade.ClearFrameState();
            }

            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                // call viewmodel
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    // prepare for state load
                    dataContext.NavigationService = this;
                    var pageState = FrameFacade.GetPageStateContainer(page.GetType());
                    dataContext.OnNavigatedTo(parameter, mode, pageState);
                }
            }
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
            
            if ((page.FullName.Equals(_lastNavigationType)) && 
                ((parameter == null && _lastNavigationParameter == null) || (parameter != null && parameter.Equals(_lastNavigationParameter))))
                return false;

            var result = FrameFacade.Navigate(page, parameter);
            return result;
        }

        /// <summary>
        /// Save the navigation state.
        /// </summary>
        public void SaveNavigation()
        {
            // it is possible to close the application before we have navigated and created state
            if (CurrentPageType == null)
                return;

            var state = FrameFacade.GetPageStateContainer(GetType());
            if (state == null)
            {
                throw new InvalidOperationException("State container is unexpectedly null");
            }

            state[CURRENT_PAGE_TYPE_KEY] = CurrentPageType.ToString();
            try { state[CURRENT_PAGE_PARAM_KEY] = CurrentPageParam; }
            catch
            {
                throw new Exception("Failed to serialize page parameter, override/implement ToString()");
            }
            state[NAVIGATE_STATE_KEY] = FrameFacade?.GetNavigationState();
        }

        public event TypedEventHandler<Type> AfterRestoreSavedNavigation;

        /// <summary>
        /// Restores the saved navigation state.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        public bool RestoreSavedNavigation()
        {
            try
            {
                var state = FrameFacade.GetPageStateContainer(GetType());
                if (state == null || !state.Any() || !state.ContainsKey(CURRENT_PAGE_TYPE_KEY))
                {
                    return false;
                }
                
                string currentPageType = state[CURRENT_PAGE_TYPE_KEY].ToString();
                Type pageTypeOfAppAssembly = Type.GetType(string.Format("{0}, {1}", currentPageType, UniversalApp.AppAssemblyName));

                FrameFacade.CurrentPageType = pageTypeOfAppAssembly;
                FrameFacade.CurrentPageParam = state[CURRENT_PAGE_PARAM_KEY];
                FrameFacade.SetNavigationState(state[NAVIGATE_STATE_KEY].ToString());

                while (FrameFacade.Frame.Content == null) { /* wait */ }

                AfterRestoreSavedNavigation?.Invoke(this, FrameFacade.CurrentPageType);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex, "Could not restore app state");
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
            try
            {
                FrameFacade.Frame.BackStack.Clear();

                // force update the shell back button after the history was cleared
                UniversalApp.Current.UpdateShellBackButton();
            }
            catch (Exception e)
            {
                Logger.WriteLine("CLEAR HISTORY FAILED!", e);
            }
            
        }

        public void Resuming(){ } // TODO: FIXME: not referenced and empty. Delete? Check again after some more progress in Template10!

        /// <summary>
        /// When suspending, it saves the navigation state and fires the navigate from event.
        /// </summary>
        public async Task SuspendingAsync()
        {
            SaveNavigation();
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
        public object CurrentPageParam
        {
            get
            {
                return FrameFacade.CurrentPageParam;
            }
        }
    }
}
