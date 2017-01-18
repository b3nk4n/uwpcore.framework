using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Devices;
using UWPCore.Framework.Input;
using UWPCore.Framework.IoC;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.UI;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
        /// Gets the inversion of control container.
        /// </summary>
        public static IInjector Injector { get; private set; }

        public static new UniversalApp Current { get; private set; }

        public StateItems SessionState { get; set; } = new StateItems();

        private IStatusBarService _statusBarService;

        /// <summary>
        /// Indicates whether the app shell should be populated.
        /// </summary>
        public bool UseAppShell { get; private set; }

        /// <summary>
        /// Gets or sets the theme color of the apps title bar.
        /// </summary>
        public IAppColorProperties ColorPropertiesDark { get; set; }

        /// <summary>
        /// Gets or sets the theme color of the apps title bar.
        /// </summary>
        public IAppColorProperties ColorPropertiesLight { get; set; }

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
        /// Gets or sets the Share Target page type.
        /// </summary>
        /// <remarks>Do not forget to set the declaration in the manifest!</remarks>
        protected Type ShareTargetPage { get; set; }

        /// <summary>
        /// Indicator tag for a frame which is in Share Target context.
        /// </summary>
        public const string FRAME_IN_SHARE_CONTEXT = "shareTarget";

        /// <summary>
        /// Creates a new UniversalApp instance.
        /// </summary>
        /// <param name="defaultPage">The default page to navigate to when the app is started.</param>
        /// <param name="backButtonBehaviour">The back button behaviour on the root level.</param>
        /// <param name="useAppShell">Defines whether the app shell is used.</param>
        /// <param name="modules">The ninject modules.</param>
        public UniversalApp(Type defaultPage, AppBackButtonBehaviour backButtonBehaviour, bool useAppShell, params NinjectModule[] modules)
        {
            Current = this;
            DefaultPage = defaultPage;
            BackButtonBehaviour = backButtonBehaviour;
            UseAppShell = useAppShell;
            AppAssemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;

            Injector = IoC.Injector.Instance;

            if (modules == null)
                Injector.Init(new DefaultModule());
            else
                Injector.Init(modules);

            Resuming += (s, e) =>
            {
                Logger.WriteLine("RESUMING");
                OnResuming(e);

                var rootPage = NavigationService.FrameFacade.Content as UniversalPage;

                if (rootPage != null)
                {
                    rootPage.OnResume();
                }
            };
            Suspending += async (s, e) =>
            {
                var globalDeferral = e.SuspendingOperation.GetDeferral();
                Logger.WriteLine("SUSPENDING");
                try
                {
                    // call system-level suspend
                    await OnSuspendingAsync(e);

                    foreach (var service in WindowWrapper.ActiveWrappers.SelectMany(x => x.NavigationServices))
                    {
                        try
                        {
                            // date the cache (which marks the date/time it was suspended)
                            service.FrameFacade.SetFrameState(CACHE_DATE_KEY, DateTime.Now.ToString());

                            // call view model suspend (OnNavigatedfrom)
                            await service.SuspendingAsync();
                        }
                        catch (Exception ex)
                        {
                            // FrameFacade.SetFrameState() causes en error sometimes:
                            // State Manager failed to write the setting. (Exception from HRESULT: 0x80073DC0)
                            Logger.WriteLine(ex, "Suspending failed: FrameFacade.SetFrameState(CACHE_DATE_KEY)");
                        }
                    }
                }
                finally
                {
                    globalDeferral.Complete();
                }
            };

            // handle exceptions from external libraries
            UnhandledException += (s, e) =>
            {
                if (e != null)
                {
                    Exception exception = e.Exception;

                    // smatoo advertising (recommanded by MSDN)
                    if (exception is NullReferenceException && exception.ToString().ToUpper().Contains("SOMA"))
                    {
                        Logger.WriteLine("Handled Smaato null reference exception {0}", exception);
                        e.Handled = true;
                        return;
                    }
                }

                // APP SPECIFIC HANDLING HERE

                if (Debugger.IsAttached)
                {
                    // An unhandled exception has occurred; break into the debugger
                    Debugger.Break();
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
        /// This is the NavigationService of the primary, first, or default Frame. 
        /// An app can contain many frames and reference to this NavigationService should
        /// not be confused as the NavigationService to the "current" Frame as
        /// it is only a helper property to provide the NavigationService for
        /// the first Frame ultimately aggregated in the static WindowWrapper class.
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

        private IKeyboardService _keyboardService;

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
        /// This handles all the prelimimary stuff unique to Activated before calling OnStartAsync()
        /// This is private because it is a specialized prelude to OnStartAsync().
        /// OnStartAsync will not be called if state restore is determined.
        /// </summary>
        /// <param name="e">The event args.</param>
        private async Task InternalActivatedAsync(IActivatedEventArgs e)
        {
            if (e.Kind == ActivationKind.ShareTarget)
            {
                var shareArgs = e as ShareTargetActivatedEventArgs;

                if (e.PreviousExecutionState == ApplicationExecutionState.Running ||
                    e.PreviousExecutionState == ApplicationExecutionState.Suspended)
                {
                    var frame = new Frame();
                    frame.Tag = FRAME_IN_SHARE_CONTEXT;
                    Window.Current.Content = frame;

                    var view = WindowWrapper.Current(Window.Current);
                    var navigationService = new NavigationService(frame);
                    view.NavigationServices.Add(navigationService);

                    if (frame.Content == null)
                        navigationService.Navigate(ShareTargetPage, shareArgs.ShareOperation);
                }
                else
                {
                    InitRootFrameAndNavigation();
                    Window.Current.Content = RootFrame;

                    await WindowWrapper.Current(NavigationService).Dispatcher.DispatchAsync(() =>
                    {
                        NavigationService.Navigate(ShareTargetPage, shareArgs.ShareOperation);
                    });
                }

                Window.Current.Activate();
                return;
            }

            // sometimes activate requires a frame to be built, such as after a launch using a toast notification
            if (Window.Current.Content == null)
            {
                InitRootFrameAndNavigation();

                await OnInitializeAsync(e);

                _statusBarService = Injector.Get<IStatusBarService>();

                if (UseAppShell &&
                    e.PreviousExecutionState != ApplicationExecutionState.Running &&
                    e.PreviousExecutionState != ApplicationExecutionState.Suspended &&
                    e.Kind != ActivationKind.ShareTarget)
                {
                    Window.Current.Content = new AppShell(
                        RootFrame,
                        CreateNavigationMenuItems(),
                        CreateBottomDockedNavigationMenuItems());
                }
            }

            // onstart is shared with activate and launch
            await OnStartAsync(StartKind.Activate, e);

            // if the user didn't already set custom content use rootframe
            if (Window.Current.Content == null)
            {
                Window.Current.Content = RootFrame;
            }

            UpdateTheme();

            // ensure active (this will hide any custom splashscreen)
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
            bool test = Window.Current == args.Window;
            var window = new WindowWrapper(args.Window);
            WindowCreated?.Invoke(this, args);
            base.OnWindowCreated(args);
        }

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        [Obsolete("Use OnStartAsync()")]
        protected override void OnLaunched(LaunchActivatedEventArgs e) { InternalLaunchAsync(e); }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        /// <summary>
        /// The internal launch handler that is called when when the app has been launched.
        /// </summary>
        /// <param name="e">The launch activated event args.</param>
        private async void InternalLaunchAsync(ILaunchActivatedEventArgs e)
        {
            // now handle normal activation
            UIElement splashScreen = default(UIElement);
            if (SplashFactory != null)
            {
                splashScreen = SplashFactory(e.SplashScreen);
                Window.Current.Content = splashScreen;
            }

            InitRootFrameAndNavigation();

            // expire state (based on expiry)
            DateTime cacheDate;
            var otherwise = DateTime.MinValue.ToString();
            if (DateTime.TryParse(NavigationService.FrameFacade.GetFrameState(CACHE_DATE_KEY, otherwise), out cacheDate))
            {
                var cacheAge = DateTime.Now.Subtract(cacheDate);
                if (cacheAge >= CacheMaxDuration)
                {
                    // clear state in every nav service in every view
                    foreach (var service in WindowWrapper.ActiveWrappers.SelectMany(x => x.NavigationServices))
                    {
                        service.FrameFacade.ClearFrameState();
                    }
                }
            }
            else
            {
                // no date, also fine...
            }

            // the user may override to set custom content
            await OnInitializeAsync(e);

            _statusBarService = Injector.Get<IStatusBarService>();

            if (UseAppShell &&
                e.PreviousExecutionState != ApplicationExecutionState.Running &&
                e.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                Window.Current.Content = new AppShell(
                    RootFrame,
                    CreateNavigationMenuItems(),
                    CreateBottomDockedNavigationMenuItems());
            }

            switch (e.PreviousExecutionState)
            {
                case ApplicationExecutionState.NotRunning:
                case ApplicationExecutionState.Running:
                case ApplicationExecutionState.Suspended:
                case ApplicationExecutionState.ClosedByUser:
                    {
                        // launch if not restored
                        await OnStartAsync(StartKind.Launch, e);
                        break;
                    }
                case ApplicationExecutionState.Terminated:
                    {
                        /*
                            Restore state if you need to/can do.
                            Remember that only the primary tile should restore.
                            (this includes toast with no data payload)
                            The rest are already providing a nav path.
                            In the event that the cache has expired, attempting to restore
                            from state will fail because of missing values. 
                            This is okay & by design.
                        */
                        if (DetermineStartCause(e) == AdditionalKinds.Primary || DetermineStartCause(e) == AdditionalKinds.Toast)
                        {
                            var restored = NavigationService.RestoreSavedNavigation();
                            if (!restored)
                            {
                                await OnStartAsync(StartKind.Launch, e);
                            }
                            else
                            {
                                UpdateShellBackButton();
                            }
                        }
                        else
                        {
                            await OnStartAsync(StartKind.Launch, e);
                        }
                        break;
                    }
            }

            // if the user didn't already set custom content use rootframe
            if (Window.Current.Content == null || Window.Current.Content == splashScreen)
            {
                Window.Current.Content = RootFrame;
            }

            // Update the app theme
            UpdateTheme();

            // ensure active
            Window.Current.Activate();
        }

        /// <summary>
        /// Creates the main app shell navigation buttons.
        /// </summary>
        /// <returns>The app shell navigation buttons.</returns>
        protected virtual IEnumerable<NavMenuItem> CreateNavigationMenuItems() { return null; }

        /// <summary>
        /// Creates the secondary app shell navigation buttons at the bottom.
        /// </summary>
        /// <returns>The app shell navigation buttons.</returns>
        protected virtual IEnumerable<NavMenuItem> CreateBottomDockedNavigationMenuItems() { return null; }

        /// <summary>
        /// Inits the root frame and the navigation.
        /// </summary>
        private void InitRootFrameAndNavigation()
        {
            // setup frame
            if (RootFrame == null)
            {
                RootFrame = new Frame
                {
                    Language = ApplicationLanguages.Languages[0]
                };
                RootFrame.Navigated += (s, args) =>
                {
                    UpdateShellBackButton();
                };

                // register back button
                SystemNavigationManager.GetForCurrentView().BackRequested += (s, args) =>
                {
                    
                    var handledByAppShell = false;
                    if (UseAppShell)
                    {
                        var appshell = Window.Current.Content as AppShell;
                        if (appshell != null)
                        {
                            appshell.BackRequested(ref handledByAppShell);
                        }
                    }

                    if (!handledByAppShell)
                    {
                        var handled = false;

                        if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
                        {
                            if (NavigationService.CanGoBack)
                            {
                                handled = true;
                            }
                            else if (BackButtonBehaviour == AppBackButtonBehaviour.Terminate)
                            {
                                args.Handled = true;
                                Current.Exit();
                            }
                        }
                        else
                        {
                            handled = !NavigationService.CanGoBack;
                        }

                        args.Handled = handled;
                        RaiseBackRequested(ref handled);
                    }
                    else
                    {
                        args.Handled = true;
                    }
                };

                // hook up keyboard and mouse Back handler
                _keyboardService = Injector.Get<IKeyboardService>();
                _keyboardService.AfterBackGesture = () =>
                {
                    //the result is no matter
                    var handled = false;
                    RaiseBackRequested(ref handled);
                };

                // Hook up keyboard and house Forward handler
                _keyboardService.AfterForwardGesture = RaiseForwardRequested;
            }

            // setup default view
            var view = WindowWrapper.ActiveWrappers.First();
            var navigationService = new NavigationService(RootFrame);
            view.NavigationServices.Add(navigationService);

            // requrest minimum size, to be like the MSN apps
            var applicationView = ApplicationView.GetForCurrentView();
            applicationView.SetPreferredMinSize(new Size(500, 500));
        }

        /// <summary>
        /// Updates the theme color of the title bar (pc only).
        /// </summary>
        /// <param name="theme">The theme to use.</param>
        private void UpdateTitleBarTheme(ApplicationTheme theme)
        {
            IAppColorProperties colorProperties = (theme == ApplicationTheme.Light) ? ColorPropertiesLight : ColorPropertiesDark;

            if (colorProperties == null)
                return;

            var coreTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            coreTitleBar.BackgroundColor = colorProperties.TitleBarBackground;
            coreTitleBar.ButtonBackgroundColor = colorProperties.TitleBarBackground;
            coreTitleBar.ButtonForegroundColor = colorProperties.TitleBarForeground;
            coreTitleBar.ButtonHoverBackgroundColor = colorProperties.Theme;
            coreTitleBar.ButtonHoverForegroundColor = Colors.White;
            coreTitleBar.ButtonInactiveBackgroundColor = colorProperties.TitleBarBackground;
            coreTitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            coreTitleBar.ButtonPressedBackgroundColor = Colors.DarkGray;
            coreTitleBar.ButtonPressedForegroundColor = Colors.White;
            coreTitleBar.ForegroundColor = colorProperties.TitleBarForeground;
            coreTitleBar.InactiveBackgroundColor = colorProperties.TitleBarBackground;
            coreTitleBar.InactiveForegroundColor = Colors.Gray;
        }

        /// <summary>
        /// Updates the theme color of the status bar (phone only).
        /// </summary>
        /// <param name="theme">The theme to use.</param>
        public void UpdateStatusBarTheme(ApplicationTheme theme)
        {
            if (_statusBarService.IsSupported)
            {
                IAppColorProperties colorProperties = (theme == ApplicationTheme.Light) ? ColorPropertiesLight : ColorPropertiesDark;

                if (colorProperties == null)
                    return;

                if (colorProperties.StatusBarBackground.HasValue)
                    _statusBarService.BackgroundColor = colorProperties.StatusBarBackground.Value;
                if (colorProperties.StatusBarForeground.HasValue)
                    _statusBarService.ForegroundColor = colorProperties.StatusBarForeground.Value;
            }
        }

        /// <summary>
        /// Updates the theme color of the app shell hamburger.
        /// </summary>
        /// <param name="theme">The theme to use.</param>
        public void UpdateAppShellHamburgerTheme(ApplicationTheme theme)
        {
            IAppColorProperties colorProperties = (theme == ApplicationTheme.Light) ? ColorPropertiesLight : ColorPropertiesDark;

            if (colorProperties == null)
                return;

            if (UseAppShell)
            {
                var appshell = Window.Current.Content as AppShell;
                if (appshell != null)
                {
                    appshell.SetToggleButtonColors(colorProperties.AppShellHamburgerForeground, colorProperties.AppShellHamburgerBackground);
                }
            }
        }

        /// <summary>
        /// Updates the app theme.
        /// </summary>
        public void UpdateTheme()
        {
            var theme = ApplicationTheme;

            UpdateTitleBarTheme(theme);
            UpdateStatusBarTheme(theme);
            UpdateAppShellHamburgerTheme(theme);
        }

        public ApplicationTheme ApplicationTheme
        {
            get
            {
                UniversalPage rootPage;

                if (UseAppShell)
                    rootPage = Window.Current.Content as UniversalPage;
                else
                    rootPage = (Window.Current.Content as Frame).Content as UniversalPage;


                if (rootPage == null)
                    return RequestedTheme;

                // ensure the app theme is updated
                rootPage.UpdateTheme();

                if (rootPage.RequestedTheme == ElementTheme.Light ||
                    rootPage.RequestedTheme == ElementTheme.Default && RequestedTheme == ApplicationTheme.Light)
                    return ApplicationTheme.Light;
                else
                    return ApplicationTheme.Dark;
            }
        }


        /// <summary>
        /// Default Hardware/Shell Back handler overrides standard Back behavior 
        /// that navigates to previous app in the app stack to instead cause a backward page navigation.
        /// Views or Viewodels can override this behavior by handling the BackRequested 
        /// event and setting the Handled property of the BackRequestedEventArgs to true.
        /// </summary>
        private void RaiseBackRequested(ref bool handled)
        {
            var args = new HandledEventArgs();

            foreach (var frame in WindowWrapper.Current().NavigationServices.Select(x => x.FrameFacade))
            {
                frame.RaiseBackRequested(args);
                handled = args.Handled;
                if (handled)
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
            foreach (var frame in WindowWrapper.Current().NavigationServices.Select(x => x.FrameFacade))
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
        /// The hook method that is invoked when the app has started.
        /// </summary>
        /// <param name="startKind">The start up kind.</param>
        /// <param name="args">The activated event args.</param>
        public abstract Task OnStartAsync(StartKind startKind, IActivatedEventArgs args);

        /// <summary>
        /// The hook method to perform some initialization work, such as registering
        /// the Shell frame wrapper class.
        /// </summary>
        /// <param name="args">The activation event args.</param>
        public virtual Task OnInitializeAsync(IActivatedEventArgs args)
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

        public const string DefaultTileID = "App";

        public void UpdateShellBackButton()
        {
            // show the shell back only if there is anywhere to go in the default frame
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                (ShowShellBackButton && NavigationService.FrameFacade.CanGoBack)
                    ? AppViewBackButtonVisibility.Visible
                    : AppViewBackButtonVisibility.Collapsed;
        }

        public enum AdditionalKinds { Primary, Toast, SecondaryTile, Other }

        /// <summary>
        /// This determines the simplest case for starting. This should handle 80% of common scenarios. 
        /// When Other is returned the developer must determine start manually using IActivatedEventArgs.Kind
        /// </summary>
        public static AdditionalKinds DetermineStartCause(IActivatedEventArgs args)
        {
            var e = args as ILaunchActivatedEventArgs;
            if (args is ToastNotificationActivatedEventArgs)
                return AdditionalKinds.Toast;
            if (e?.TileId == DefaultTileID && string.IsNullOrEmpty(e?.Arguments))
                return AdditionalKinds.Primary;
            else if (e?.TileId != null && e?.TileId != DefaultTileID)
                return AdditionalKinds.SecondaryTile;
            else
                return AdditionalKinds.Other;
        }
    }
}
