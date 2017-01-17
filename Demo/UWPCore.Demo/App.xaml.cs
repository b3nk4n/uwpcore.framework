using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Speech;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using UWPCore.Framework.IoC;
using UWPCore.Framework.UI;
using System.Collections.Generic;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        ISpeechService _speechService;

        public App() : base(typeof(MainPage), AppBackButtonBehaviour.KeepAlive, true, new DefaultModule())
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

            // set theme colors
            var BLUE_THEME = Color.FromArgb(255, 0, 34, 119);
            ColorPropertiesDark = new AppColorProperties(BLUE_THEME, Colors.White, Colors.Black, null, null, Colors.White, BLUE_THEME);
            ColorPropertiesLight = new AppColorProperties(Colors.Red, Colors.Black, Colors.White, null, null, Colors.Black, null);

            _speechService = Injector.Get<ISpeechService>();
            //await _speechService.InstallCommandSets("/Assets/Speech/AdventureWorksCommands.xml"); // hangs in 10586 on  mobile !?
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

        protected override IEnumerable<NavMenuItem> CreateNavigationMenuItems()
        {
            return new[]
            {
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.HomeOutline,
                    Label = "Home",
                    DestinationPage = typeof(MainPage)
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
                    Symbol = GlyphIcons.ArrowRight,
                    Label = "Navigation",
                    DestinationPage = typeof(NavigationPage1)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.FolderPlus,
                    Label = "Backup",
                    DestinationPage = typeof(OneDrivePage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Contact,
                    Label = "User Data",
                    DestinationPage = typeof(UserDataPage)
                }
            };
        }

        protected override IEnumerable<NavMenuItem> CreateBottomDockedNavigationMenuItems()
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
