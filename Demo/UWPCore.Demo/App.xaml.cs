using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Navigation;
using Windows.ApplicationModel.Activation;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        public App()
        {
            InitializeComponent();
            ShowShellBackButton = true;

            // initialize Microsoft Application Insights
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
        }

        public override Task OnInitializeAsync()
        {
            // runs before everything
            return base.OnInitializeAsync();
        }

        public override void OnPrelaunch()
        {
            // handle prelaunch
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // start the user experience
            NavigationService.Navigate(typeof(MainPage));
            return Task.FromResult<object>(null);
        }
    }
}
