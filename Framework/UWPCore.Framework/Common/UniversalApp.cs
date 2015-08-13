using System;
using System.Linq;
using System.Threading.Tasks;
using UWPCore.Framework.Input;
using UWPCore.Framework.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Common
{
    /// <summary>
    /// The base class of an Universal Windows Platform app that is based on the UWPCore framework.
    /// </summary>
    public abstract class UniversalApp : Application
    {
        /// <summary>
        /// The assembly name of the application to be implemented by the framework user.
        /// </summary>
        public static string AppAssemblyName { get; private set; }

        /// <summary>
        /// Indicates the BACK button behaviour of the initial page.
        /// </summary>
        public static AppBackButtonBehaviour BackButtonBehaviour { get; private set; }

        /// <summary>
        /// The back button behaviour of the app on the root level.
        /// </summary>
        public enum AppBackButtonBehaviour
        {
            Terminate,
            KeepAlive
        }

        /// <summary>
        /// The app start kind.
        /// </summary>
        public enum StartKind
        {
            Launch,
            Activate
        }

        /// <summary>
        /// Creates a new UniversalApp instance.
        /// </summary>
        /// <param name="defaultPage">The default page to navigate to when the app is started.</param>
        /// <param name="backButtonBehaviour">The back button behaviour on the root level.</param>
        /// <param name="appAssemblyName">The applicatoins assembly name implememnted by the framework user.</param>
        public UniversalApp(Type defaultPage, AppBackButtonBehaviour backButtonBehaviour, string appAssemblyName)
        {
            DefaultPage = defaultPage;
            AppAssemblyName = appAssemblyName;

            Resuming += (s, e) => { OnResuming(e); };
            Suspending += async (s, e) =>
            {
                var globalDeferral = e.SuspendingOperation.GetDeferral();
                try
                {
                    foreach (var service in WindowWrapper.ActiveWrappers.SelectMany(x => x.NavigationServices))
                    {
                        // date the cache (which marks the date/time it was suspended)
                        service.Frame.SetFrameState(CACHE_DATE_KEY, DateTime.Now.ToString());
                        // call view model suspend (OnNavigatedfrom)
                        await service.SuspendingAsync();
                    }
                    // call system-level suspend
                    await OnSuspendingAsync(e);
                }
                finally
                {
                    globalDeferral.Complete();
                }
            };
        }

        #region properties

        /// <summary>
        /// The default page to be navigated to when the app is launched.
        /// </summary>
        public Type DefaultPage { get; private set; }

        /// <summary>
        /// The current root frame.
        /// </summary>
        public Frame RootFrame { get; set; }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        public NavigationService NavigationService
        {
            // because it is protected, we can safely assume it will ref the first view
            get { return WindowWrapper.ActiveWrappers.First().NavigationServices.First(); }
        }

        /// <summary>
        /// Gets or sets the splash screen factory.
        /// </summary>
        protected Func<SplashScreen, Page> SplashFactory { get; set; }

        /// <summary>
        /// The maximum duration of the frame state cache container.
        /// </summary>
        public TimeSpan CacheMaxDuration { get; set; } = TimeSpan.MaxValue;

        /// <summary>
        /// The cache date key.
        /// </summary>
        private const string CACHE_DATE_KEY = "Setting-Cache-Date";

        /// <summary>
        /// Gets or sets whether the shell back button is visible.
        /// </summary>
        public bool ShowShellBackButton { get; set; } = true;

        #endregion

        #region activated

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        [Obsolete("Use OnStartAsync()")]
        protected override async void OnActivated(IActivatedEventArgs e) { await InternalActivatedAsync(e); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args) { await InternalActivatedAsync(args); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnFileActivated(FileActivatedEventArgs args) { await InternalActivatedAsync(args); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args) { await InternalActivatedAsync(args); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args) { await InternalActivatedAsync(args); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnSearchActivated(SearchActivatedEventArgs args) { await InternalActivatedAsync(args); }

        [Obsolete("Use OnStartAsync()")]
        protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args) { await InternalActivatedAsync(args); }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        /// <summary>
        /// The internal actived handler that is called whenever the app is activated.
        /// </summary>
        /// <param name="e">The event args.</param>
        private async Task InternalActivatedAsync(IActivatedEventArgs e)
        {
            await OnStartAsync(StartKind.Activate, e);
            Window.Current.Activate();
        }

        #endregion

        /// <summary>
        /// The windows created event.
        /// </summary>
        public event EventHandler<WindowCreatedEventArgs> WindowCreated;

        /// <summary>
        /// The windows created event handler.
        /// </summary>
        /// <param name="args">The event args.</param>
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            var window = new WindowWrapper(args.Window);
            WindowCreated?.Invoke(this, args);
            base.OnWindowCreated(args);
        }

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        [Obsolete("Use OnStartAsync()")]
        protected override void OnLaunched(LaunchActivatedEventArgs e) { InternalLaunchAsync(e as ILaunchActivatedEventArgs); }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        /// <summary>
        /// The internal launch handler that is called when when the app has been launched.
        /// </summary>
        /// <param name="e">The launch activated event args.</param>
        private async void InternalLaunchAsync(ILaunchActivatedEventArgs e)
        {
            // first handle prelaunch, which will not continue
            if ((e.Kind == ActivationKind.Launch) && ((e as LaunchActivatedEventArgs)?.PrelaunchActivated ?? false))
            {
                OnPrelaunch();
                return;
            }

            // now handle normal activation
            UIElement splashScreen = default(UIElement);
            if (SplashFactory != null)
            {
                splashScreen = SplashFactory(e.SplashScreen);
                Window.Current.Content = splashScreen;
            }

            // setup frame
            RootFrame = new Frame
            {
                Language = ApplicationLanguages.Languages[0]
            };
            RootFrame.Navigated += (s, args) =>
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    (ShowShellBackButton && RootFrame.CanGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            };

            // setup default view
            var view = WindowWrapper.ActiveWrappers.First();
            var navigationService = new NavigationService(RootFrame);
            view.NavigationServices.Add(navigationService);

            // expire state (based on expiry)
            DateTime cacheDate;
            var otherwise = DateTime.MinValue.ToString();
            if (DateTime.TryParse(navigationService.Frame.GetFrameState(CACHE_DATE_KEY, otherwise), out cacheDate))
            {
                var cacheAge = DateTime.Now.Subtract(cacheDate);
                if (cacheAge >= CacheMaxDuration)
                {
                    // clear state in every nav service in every view
                    foreach (var service in WindowWrapper.ActiveWrappers.SelectMany(x => x.NavigationServices))
                    {
                        service.Frame.ClearFrameState();
                    }
                }
            }
            else
            {
                // no date, also fine...
            }

            // the user may override to set custom content
            await OnInitializeAsync();
            switch (e.PreviousExecutionState)
            {
                case ApplicationExecutionState.NotRunning:
                case ApplicationExecutionState.Running:
                case ApplicationExecutionState.Suspended:
                    {
                        // launch if not restored
                        await OnStartAsync(StartKind.Launch, e);
                        break;
                    }
                case ApplicationExecutionState.ClosedByUser:
                case ApplicationExecutionState.Terminated:
                    {
                        // restore if you need to/can do
                        var restored = navigationService.RestoreSavedNavigationState();
                        if (!restored)
                        {
                            await OnStartAsync(StartKind.Launch, e);
                        }
                        break;
                    }
            }

            // if the user didn't already set custom content use rootframe
            if (Window.Current.Content == splashScreen) { Window.Current.Content = RootFrame; }
            if (Window.Current.Content == null) { Window.Current.Content = RootFrame; }

            // ensure active
            Window.Current.Activate();

            // Hook up the default Back handler
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, args) =>
            {
                // TODO: handled=true canisn't true at end of backstack
                if (navigationService.CanGoBack)
                {
                    args.Handled = true;
                    RaiseBackRequested();
                }
                else if (BackButtonBehaviour == AppBackButtonBehaviour.Terminate)
                {
                    Current.Exit();
                }
            };

            // Hook up keyboard and mouse Back handler
            var keyboard = new KeyboardService();
            keyboard.AfterBackGesture = () => RaiseBackRequested();

            // Hook up keyboard and house Forward handler
            keyboard.AfterForwardGesture = () => RaiseForwardRequested();
        }

        /// <summary>
        /// Default hardware/shell BACK handler overrides standard BACK behavior that navigates to previous app
        /// in the app stack to instead cause a backward page navigation.
        /// Views or Viewodels can override this behavior by handling the BackRequested event and setting the
        /// Handled property of the BackRequestedEventArgs to true.
        /// </summary>
        private void RaiseBackRequested()
        {
            var args = new HandledEventArgs();
            foreach (var frame in WindowWrapper.Current().NavigationServices.Select(x => x.Frame))
            {
                frame.RaiseBackRequested(args);
                if (args.Handled)
                    return;
            }

            // default to first window
            NavigationService.GoBack();
        }

        /// <summary>
        /// Default shell FORWARD handler overrides standard FORWARD behavior that navigates to next page
        /// in the stack. Views or Viewodels can override this behavior by handling the ForwardRequested event
        /// and setting the Handled property of the ForwardRequestedEventArgs to true.
        /// </summary>
        private void RaiseForwardRequested()
        {
            var args = new HandledEventArgs();
            foreach (var frame in WindowWrapper.Current().NavigationServices.Select(x => x.Frame))
            {
                frame.RaiseForwardRequested(args);
                if (args.Handled)
                    return;
            }

            // default to first window
            NavigationService.GoForward();
        }

        #region overrides

        /// <summary>
        /// The hook method that is invoked before the app has launched.
        /// </summary>
        public virtual void OnPrelaunch() { }

        /// <summary>
        /// The hook method that is invoked when the app has started.
        /// </summary>
        /// <param name="startKind">The start up kind.</param>
        /// <param name="args">The activated event args.</param>
        public abstract Task OnStartAsync(StartKind startKind, IActivatedEventArgs args);

        /// <summary>
        /// The hook method to perform some initialization work, such as registering
        /// the Shell frame wrapper class.
        /// </summary>
        public virtual Task OnInitializeAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The hook method that is invoked when the app is suspended.
        /// </summary>
        /// <param name="e">The suspending event args.</param>
        public virtual Task OnSuspendingAsync(SuspendingEventArgs e)
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The hook method that is invoked when the app is resumed.
        /// </summary>
        /// <param name="args">The resuming argument.</param>
        public virtual void OnResuming(object args) { }

        #endregion
    }
}
