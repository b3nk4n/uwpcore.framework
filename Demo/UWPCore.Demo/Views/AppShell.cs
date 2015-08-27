using System.Collections.Generic;
using UWPCore.Framework.Controls;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// App shell implementations that 
    /// </summary>
    public class AppShell : ShellBase
    {
        public AppShell(Frame frame)
            : base(frame)
        {
        }

        public override IEnumerable<NavMenuItem> GetNavigationItems()
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
                    Label = "Tasks",
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
