using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Speech;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using UWPCore.Framework.Devices;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        ISpeechService _speechService;

        IStatusBarService _statusBarService;

        public App() : base(typeof(MainPage), AppBackButtonBehaviour.Terminate, "UWPCore.Demo")
        {
            InitializeComponent();

            ShowShellBackButton = true;

            // initialize Microsoft Application Insights
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
        }

        public async override Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            // remove this line to hide the SplitView-Shell
            Window.Current.Content = new AppShell(RootFrame, GetNavigationMenuItems());

            _speechService = new SpeechService();
            await _speechService.InstallCommandSets("/Assets/Speech/AdventureWorksCommands.xml");

            _statusBarService = new StatusBarService();
            var color = (Color)Current.Resources["SystemChromeMediumColor"];
            _statusBarService.BackgroundColor = color;
        }

        public override void OnPrelaunch()
        {
            // handle prelaunch
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args, ILaunchArgs launchArgs)
        {
            // check lauch arguments
            if (launchArgs.IsValid)
            { 
                Logger.WriteLine("Started with launch args: args->{0}; tileId->{1}", launchArgs.Arguments, launchArgs.TileId);
            }

            // check voice commands
            var command = _speechService.GetVoiceCommand(args);
            if (command != null)
            {
                switch(command.CommandName)
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
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestinationPage = typeof(MainPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Audio,
                    Label = "Audio",
                    DestinationPage = typeof(AudioPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Keyboard,
                    Label = "Device Features",
                    DestinationPage = typeof(DeviceFeaturesPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.TwoBars,
                    Label = "Networking",
                    DestinationPage = typeof(NetworkingPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Message,
                    Label = "Notifications",
                    DestinationPage = typeof(NotificationsPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.People,
                    Label = "Speech",
                    DestinationPage = typeof(SpeechPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Shuffle,
                    Label = "Share",
                    DestinationPage = typeof(SharePage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.FontColor,
                    Label = "Graphics",
                    DestinationPage = typeof(GraphicsPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Document,
                    Label = "MVVM",
                    DestinationPage = typeof(MvvmPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Clock,
                    Label = "Tasks",
                    DestinationPage = typeof(TasksPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Play,
                    Label = "Launcher",
                    DestinationPage = typeof(LaunchPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Help,
                    Label = "About",
                    DestinationPage = typeof(AboutPage)
                },
                new NavMenuItem() // TODO: make it possible to stack navItems/links to the bottom
                {
                    Symbol = Symbol.Setting,
                    Label = "Settings",
                    DestinationPage = typeof(SettingsPage)
                }
            };
        }
    }
}
