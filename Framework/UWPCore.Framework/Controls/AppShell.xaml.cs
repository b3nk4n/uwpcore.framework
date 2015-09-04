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
    public partial class AppShell : Page
    {
        /// <summary>
        /// The value where the adaptive UI chagnes its state.
        /// </summary>
        private const int ADAPTIVE_UI_TRIGGER_WIDTH = 720;

        /// <summary>
        /// The declared top level navigation items list.
        /// </summary>
        public IEnumerable<NavMenuItem> NavigationItems { get; private set; }

        /// <summary>
        /// Singlton access to the current app shell.
        /// </summary>
        public static AppShell Current = null;

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
        public AppShell(Frame frame, IEnumerable<NavMenuItem> navigationItems)
        {
            InitializeComponent();
            RootSplitView.Content = frame;
            _rootFrame = frame;

            _rootFrame.Navigating += OnNavigatingToPage;
            _rootFrame.Navigated += OnNavigatedToPage;

            Loaded += (sender, args) =>
            {
                Current = this;

                TogglePaneButton.Focus(FocusState.Programmatic);
            };

            SizeChanged += (s, e) =>
            {
                // manually update toggle pane button size when the size was changted to small mode.
                if (e.NewSize.Width <= ADAPTIVE_UI_TRIGGER_WIDTH)
                {
                    CheckTogglePaneButtonSizeChanged();
                }
            };

            // If on a phone device that has hardware buttons then we hide the app's back button.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                BackButton.Visibility = Visibility.Collapsed;
            }

            NavigationItems = navigationItems;
            NavMenuList.ItemsSource = NavigationItems;
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
                var item = (from p in NavigationItems where p.DestinationPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null && AppFrame.BackStackDepth > 0)
                {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in AppFrame.BackStack.Reverse())
                    {
                        item = (from p in NavigationItems where p.DestinationPage == entry.SourcePageType select p).SingleOrDefault();
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
        public event TypedEventHandler<AppShell, Rect> TogglePaneButtonRectChanged;

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
                //var transform = TogglePaneButton.TransformToVisual(this);
                //var rect = transform.TransformBounds(new Rect(0, 0, TogglePaneButton.ActualWidth, TogglePaneButton.ActualHeight));
                var rect = new Rect(0, 0, TogglePaneButton.ActualWidth, TogglePaneButton.ActualHeight);
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
