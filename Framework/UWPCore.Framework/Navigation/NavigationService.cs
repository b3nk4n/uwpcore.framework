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
        /// The empty navigation value.
        /// </summary>
        private const string EMPTY_NAVIGATION = "1,0";

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
        public FrameFacade Frame { get; private set; }

        /// <summary>
        /// The last navigation parameter.
        /// </summary>
        private string _lastNavigationParameter;

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
            Frame = new FrameFacade(frame);
            Frame.Navigating += async (s, e) =>
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
            Frame.Navigated += (s, e) =>
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
            var page = Frame.Content as Page;
            if (page != null)
            {
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    var args = new NavigatingEventArgs
                    {
                        PageType = Frame.CurrentPageType,
                        Parameter = Frame.CurrentPageParam,
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
            var page = Frame.Content as Page;
            if (page != null)
            {
                // call viewmodel
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    dataContext.Identifier = string.Format("Page- {0}", Frame.BackStackDepth);
                    var pageState = Frame.GetPageStateContainer(page.GetType());
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
            _lastNavigationType = Frame.Content.GetType().FullName;

            if (mode == NavigationMode.New)
            {
                Frame.ClearFrameState();
            }

            var page = Frame.Content as Page;
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
                        var pageState = Frame.GetPageStateContainer(page.GetType());
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
        public async Task<int> OpenAsync(Type page, string parameter = null, ViewSizePreference size = ViewSizePreference.UseHalf)
        {
            // TODO: this will spawn a new window instead of navigating to an existing frame.

            var coreView = CoreApplication.CreateNewView();
            ApplicationView view = null;
            var create = new Action(() =>
            {
                // setup content
                var frame = new Frame();
                frame.NavigationFailed += (s, e) => { System.Diagnostics.Debugger.Break(); };
                frame.Navigate(page, parameter);

                // create window
                var window = Window.Current;
                window.Content = frame;

                // setup view/collapse
                view = ApplicationView.GetForCurrentView();
                Windows.Foundation.TypedEventHandler<ApplicationView, ApplicationViewConsolidatedEventArgs> consolidated = null;
                consolidated = new Windows.Foundation.TypedEventHandler<ApplicationView, ApplicationViewConsolidatedEventArgs>((s, e) =>
                {
                    (s as ApplicationView).Consolidated -= consolidated;
                    if (CoreApplication.GetCurrentView().IsMain)
                        return;
                    try { window.Close(); }
                    finally { CoreApplication.GetCurrentView().CoreWindow.Activate(); }
                });
                view.Consolidated += consolidated;
            });

            // execute create
            await WindowWrapper.Current().Dispatcher.DispatchAsync(create);

            // show view
            if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(view.Id, size))
            {
                // change focus
                await ApplicationViewSwitcher.SwitchAsync(view.Id);
            }
            return view.Id;
        }

        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <param name="page">The page type.</param>
        /// <param name="parameter">The navigation parameter.</param>
        /// <returns>Returns True for success, else False.</returns>
        public bool Navigate(Type page, string parameter = null)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            if (page.FullName.Equals(_lastNavigationType)
                && parameter == _lastNavigationParameter)
                return false;
            return Frame.Navigate(page, parameter);
        }

        /// <summary>
        /// Save the navigation state.
        /// </summary>
        public void SaveNavigationState()
        {
            var state = Frame.GetPageStateContainer(GetType());
            state[CURRENT_PAGE_TYPE_KEY] = CurrentPageType.ToString();
            state[CURRENT_PAGE_PARAM_KEY] = CurrentPageParam;
            state[NAVIGATE_STATE_KEY] = Frame.GetNavigationState();
        }

        /// <summary>
        /// Restores the saved navigation state.
        /// </summary>
        /// <returns>Returns True for success, else False.</returns>
        public bool RestoreSavedNavigationState()
        {
            try
            {
                var state = Frame.GetPageStateContainer(GetType());
                string currentPageType = state[CURRENT_PAGE_TYPE_KEY].ToString();
                Type pageTypeOfAppAssembly = Type.GetType(currentPageType + ", " + UniversalApp.AppAssemblyName);

                Frame.CurrentPageType = pageTypeOfAppAssembly;
                Frame.CurrentPageParam = state[CURRENT_PAGE_PARAM_KEY]?.ToString();
                Frame.SetNavigationState(state[NAVIGATE_STATE_KEY].ToString());
                NavigateTo(NavigationMode.Refresh, Frame.CurrentPageParam);
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
            Frame.Refresh();
        }

        /// <summary>
        /// Goes back when possible.
        /// </summary>
        public void GoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        /// <summary>
        /// Gets whether we can go back in the history stack.
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return Frame.CanGoBack;
            }
        }

        /// <summary>
        /// Goes forward when possible.
        /// </summary>
        public void GoForward()
        {
            if (Frame.CanGoForward)
                Frame.GoForward();
        }

        /// <summary>
        /// Gets whether we can go forward in the history stack.
        /// </summary>
        public bool CanGoForward
        {
            get
            {
                return Frame.CanGoForward;
            }
        }

        /// <summary>
        /// Clears the history stack.
        /// </summary>
        public void ClearHistory()
        {
            Frame.SetNavigationState(EMPTY_NAVIGATION);
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
                return Frame.CurrentPageType;
            }
        }

        /// <summary>
        /// Get the current page parameter.
        /// </summary>
        public string CurrentPageParam
        {
            get
            {
                return Frame.CurrentPageParam;
            }
        }
    }
}
