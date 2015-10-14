using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Speech;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI;
using UWPCore.Framework.Devices;
using UWPCore.Framework.IoC;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        ISpeechService _speechService;

        IStatusBarService _statusBarService;

        public App() : base(typeof(MainPage), AppBackButtonBehaviour.KeepAlive, new DefaultModule())
        {
            InitializeComponent();

            // initialize Microsoft Application Insights
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
        }

        public async override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            await base.OnInitializeAsync(args);

            // only add it
            if (args.PreviousExecutionState != ApplicationExecutionState.Running &&
                args.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                Window.Current.Content = new AppShell(
                    RootFrame,
                    GetNavigationMenuItems(),
                    GetBottomDockedNavigationMenuItems());
            }

            _speechService = Injector.Get<ISpeechService>();
            await _speechService.InstallCommandSets("/Assets/Speech/AdventureWorksCommands.xml");

            _statusBarService = Injector.Get<IStatusBarService>();
            var color = (Color)Current.Resources["SystemChromeMediumColor"];
            _statusBarService.BackgroundColor = color;
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // check lauch arguments
            var launchArgs = args as ILaunchActivatedEventArgs;
            if (launchArgs != null)
            {
                if (args.Kind == ActivationKind.Launch)
                {
                    Logger.WriteLine("Started with TILE and launch args: args->{0}; tileId->{1}", launchArgs.Arguments, launchArgs.TileId);
                }
                if (args.Kind == ActivationKind.ToastNotification)
                {
                    Logger.WriteLine("Started with TOAST and launch args: args->{0}; tileId->{1}", launchArgs.Arguments, launchArgs.TileId);
                }
            }

            // check voice commands
            var command = _speechService.GetVoiceCommand(args);
            if (command != null)
            {
                switch (command.CommandName)
                {
                    case "showTripToDestination":
                        string destination = command.Interpretations["destination"];
                        Logger.WriteLine("Command: {0}, Text {1}, Interpretation: {2}", command.CommandName, command.Text, destination);
                        break;
                }
            }

            // start the user experience
            NavigationService.Navigate(DefaultPage);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Gets the navigation menu items.
        /// </summary>
        /// <returns>The navigation menu items.</returns>
        private static NavMenuItem[] GetNavigationMenuItems()
        {
            return new[]
            {
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Home,
                    Label = "Home",
                    DestinationPage = typeof(MainPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Music,
                    Label = "Audio",
                    DestinationPage = typeof(AudioPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Keyboard,
                    Label = "Device Features",
                    DestinationPage = typeof(DeviceFeaturesPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.WifiOutline2,
                    Label = "Networking",
                    DestinationPage = typeof(NetworkingPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Message,
                    Label = "Notifications",
                    DestinationPage = typeof(NotificationsPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.AccountMultiple,
                    Label = "Speech",
                    DestinationPage = typeof(SpeechPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Share,
                    Label = "Share",
                    DestinationPage = typeof(SharePage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.ComputerTheme,
                    Label = "Graphics",
                    DestinationPage = typeof(GraphicsPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.ListBlock,
                    Label = "MVVM",
                    DestinationPage = typeof(MvvmPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Clock,
                    Label = "Tasks",
                    DestinationPage = typeof(TasksPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.MediaPlay,
                    Label = "Launcher",
                    DestinationPage = typeof(LaunchPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Account,
                    Label = "Accounts",
                    DestinationPage = typeof(AccountsPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.ArrowRight,
                    Label = "Navigation",
                    DestinationPage = typeof(NavigationPage1)
                }
            };
        }

        /// <summary>
        /// Gets the navigation menu items that are docked at the bottom.
        /// </summary>
        /// <returns>The navigation menu items.</returns>
        private static NavMenuItem[] GetBottomDockedNavigationMenuItems()
        {
            return new[]
            {
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Info,
                    Label = "About",
                    DestinationPage = typeof(AboutPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Setting,
                    Label = "Settings",
                    DestinationPage = typeof(SettingsPage)
                }
            };
        }
    }
}
