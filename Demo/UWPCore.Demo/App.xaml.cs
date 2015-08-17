using System;
using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Navigation;
using UWPCore.Framework.Speech;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        SpeechService _speechService;

        public App() : base(typeof(MainPage), AppBackButtonBehaviour.Terminate, "UWPCore.Demo")
        {
            InitializeComponent();

            _speechService = new SpeechService();
            _speechService.InstallCommandSets(new Uri("ms-appx:///Assets/Speech/AdventureWorksCommands.xml"));

            ShowShellBackButton = true;

            // initialize Microsoft Application Insights
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
        }

        public override Task OnInitializeAsync()
        {
            Window.Current.Content = new Shell(RootFrame);
            return base.OnInitializeAsync();
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
    }
}
