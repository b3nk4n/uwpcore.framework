using System.Collections.Generic;
using System.Linq;
using UWPCore.Framework.Common;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The base class "chrome" layer of the app that provides top-level navigation with
    /// proper keyboarding navigation.
    /// </summary>
    public abstract partial class ShellBase : Page
    {
        /// <summary>
        /// The declared top level navigation items.
        /// </summary>
        private IEnumerable<NavMenuItem> _navList;
        /*private List<NavMenuItem> navlist = new List<NavMenuItem>(
            new[]
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
                },
            });*/

        public abstract IEnumerable<NavMenuItem> GetNavigationItems();

        /// <summary>
        /// Singlton access to the current app shell.
        /// </summary>
        public static ShellBase Current = null;

        /// <summary>
        /// The root frame, which is the same as in the navigation service.
        /// </summary>
        private Frame _rootFrame;

        /// <summary>
        /// Initializes a new instance of the AppShell, sets the static 'Current' reference,
        /// adds callbacks for Back requests and changes in the SplitView's DisplayMode, and
        /// provide the nav menu list with the data to display.
        /// </summary>
        /// <param name="frame">The root frame of the application.</param>
        public ShellBase(Frame frame)
        {
            InitializeComponent();
            RootSplitView.Content = frame;
            _rootFrame = frame;

            Loaded += (sender, args) =>
            {
                Current = this;

                TogglePaneButton.Focus(FocusState.Programmatic);
            };

            // If on a phone device that has hardware buttons then we hide the app's back button.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                BackButton.Visibility = Visibility.Collapsed;
            }

            _navList = GetNavigationItems();
            NavMenuList.ItemsSource = _navList;
        }

        /// <summary>
        /// The current app frame. The inherited <see cref="Page.Frame"/> variable is not used.
        /// </summary>
        public Frame AppFrame { get { return _rootFrame; } }

        /// <summary>
        /// Default keyboard focus movement for any unhandled keyboarding
        /// </summary>
        private void AppShell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            FocusNavigationDirection direction = FocusNavigationDirection.None;
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Left:
                case Windows.System.VirtualKey.GamepadDPadLeft:
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                case Windows.System.VirtualKey.NavigationLeft:
                    direction = FocusNavigationDirection.Left;
                    break;
                case Windows.System.VirtualKey.Right:
                case Windows.System.VirtualKey.GamepadDPadRight:
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                case Windows.System.VirtualKey.NavigationRight:
                    direction = FocusNavigationDirection.Right;
                    break;

                case Windows.System.VirtualKey.Up:
                case Windows.System.VirtualKey.GamepadDPadUp:
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                case Windows.System.VirtualKey.NavigationUp:
                    direction = FocusNavigationDirection.Up;
                    break;

                case Windows.System.VirtualKey.Down:
                case Windows.System.VirtualKey.GamepadDPadDown:
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                case Windows.System.VirtualKey.NavigationDown:
                    direction = FocusNavigationDirection.Down;
                    break;
            }

            if (direction != FocusNavigationDirection.None)
            {
                var control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(FocusState.Programmatic);
                    e.Handled = true;
                }
            }
        }

        #region BackRequested Handlers

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bool ignored = false;
            BackRequested(ref ignored);
        }

        /// <summary>
        /// Gest a hold of the current frame so that we can inspect the app back stack.
        /// </summary>
        /// <param name="handled">The handled flag.</param>
        private void BackRequested(ref bool handled)
        {
            if (AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (AppFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                AppFrame.GoBack();
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null)
            {
                if (item.DestinationPage != null &&
                    item.DestinationPage != AppFrame.CurrentSourcePageType)
                {
                    var nav = (Application.Current as UniversalApp).NavigationService;

                    // when we nav home, clear history
                    if (item.DestinationPage.Equals((Application.Current as UniversalApp).DefaultPage))
                        nav.ClearHistory();

                    // navigate only to new pages
                    if (nav.CurrentPageType != null && nav.CurrentPageType != item.DestinationPage)
                        nav.Navigate(item.DestinationPage, item.Parameter);
                }
            }
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                var item = (from p in _navList where p.DestinationPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null && AppFrame.BackStackDepth > 0)
                {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in AppFrame.BackStack.Reverse())
                    {
                        item = (from p in _navList where p.DestinationPage == entry.SourcePageType select p).SingleOrDefault();
                        if (item != null)
                            break;
                    }
                }

                var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

                // While updating the selection state of the item prevent it from taking keyboard focus.  If a
                // user is invoking the back button via the keyboard causing the selected nav menu item to change
                // then focus will remain on the back button.
                if (container != null) container.IsTabStop = false;
                NavMenuList.SetSelectedItem(container);
                if (container != null) container.IsTabStop = true;
            }
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
            CheckTogglePaneButtonSizeChanged();
        }

        #endregion

        /// <summary>
        /// Gets the toggle pane button rectangle.
        /// </summary>
        public Rect TogglePaneButtonRect
        {
            get;
            private set;
        }

        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<ShellBase, Rect> TogglePaneButtonRectChanged;

        /// <summary>
        /// Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            if (RootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                RootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = TogglePaneButton.TransformToVisual(this);
                var rect = transform.TransformBounds(new Rect(0, 0, TogglePaneButton.ActualWidth, TogglePaneButton.ActualHeight));
                TogglePaneButtonRect = rect;
            }
            else
            {
                TogglePaneButtonRect = new Rect();
            }

            var handler = TogglePaneButtonRectChanged;
            if (handler != null)
            {
                // handler(this, this.TogglePaneButtonRect);
                handler.DynamicInvoke(this, TogglePaneButtonRect);
            }
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }
    }
}
