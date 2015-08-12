﻿using System.Reflection;
using System.Threading.Tasks;
using UWPCore.Demo.Views;
using UWPCore.Framework.Common;
using UWPCore.Framework.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace UWPCore.Demo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : UniversalApp
    {
        public App() : base(typeof(MainPage), "UWPCore.Demo")
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
            Window.Current.Content = new Shell(RootFrame);
            return base.OnInitializeAsync();
        }

        public override void OnPrelaunch()
        {
            // handle prelaunch
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // start the user experience
            NavigationService.Navigate(DefaultPage);
            return Task.FromResult<object>(null);
        }
    }
}
