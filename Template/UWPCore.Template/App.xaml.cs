using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using UWPCore.Framework.IoC;
using UWPCore.Framework.Store;
using UWPCore.Framework.UI;
using UWPCore.Template.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI;

namespace UWPCore.Template
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        /// <summary>
        /// Gets the default localizer.
        /// </summary>
        private Localizer Localizer { get; set; }

        /// <summary>
        /// Gets the license service.
        /// </summary>
        private ILicenseService LicenseService { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
            : base(typeof(MainPage), AppBackButtonBehaviour.KeepAlive, false, new DefaultModule())
        {
            // initialize Microsoft Application Insights
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            InitializeComponent();

            // inject services
            LicenseService = Injector.Get<ILicenseService>();
        }

        public async override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            await base.OnInitializeAsync(args);

            // create localizer here, because the Core Windows has to be initialized first
            Localizer = new Localizer();

            // setup theme colors (mainly for title bar)
            ColorPropertiesDark = new AppColorProperties(AppConstants.COLOR_ACCENT, Colors.White, Colors.Black, Colors.White, Color.FromArgb(255, 31, 31, 31), null, null);
            ColorPropertiesLight = new AppColorProperties(AppConstants.COLOR_ACCENT, Colors.Black, Colors.White, Colors.Black, Color.FromArgb(255, 230, 230, 230), null, null);

#if DEBUG
            await LicenseService.RefeshSimulator();
#endif
        }

        public override void OnResuming(object args)
        {
            base.OnResuming(args);
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            var pageType = DefaultPage;
            object parameter = null;

            // start the user experience
            NavigationService.Navigate(pageType, parameter);

            return Task.FromResult<object>(null);
        }

        public async override Task OnSuspendingAsync(SuspendingEventArgs e)
        {
            await base.OnSuspendingAsync(e);
        }

        /// <summary>
        /// Gets the navigation menu items.
        /// </summary>
        /// <returns>The navigation menu items.</returns>
        protected override IEnumerable<NavMenuItem> CreateNavigationMenuItems()
        {
            return new[]
            {
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.HomeOutline,
                    Label = Localizer.Get("Nav.Main"),
                    DestinationPage = typeof(MainPage)
                }
            };
        }

        /// <summary>
        /// Gets the navigation menu items that are docked at the bottom.
        /// </summary>
        /// <returns>The navigation menu items.</returns>
        protected override IEnumerable<NavMenuItem> CreateBottomDockedNavigationMenuItems()
        {
            return new[]
            {
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Info,
                    Label = Localizer.Get("Nav.About"),
                    DestinationPage = typeof(AboutPage)
                },
                new NavMenuItem()
                {
                    Symbol = GlyphIcons.Setting,
                    Label = Localizer.Get("Nav.Settings"),
                    DestinationPage = typeof(SettingsPage)
                }
            };
        }
    }
}
