using System;
using System.Collections.Generic;
using System.Linq;
using UWPCore.Framework.Common;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The base class "chrome" layer of the app that provides top-level navigation with
    /// proper keyboarding navigation.
    /// </summary>
    public partial class AppShell : UniversalPage
    {
        /// <summary>
        /// The declared top level navigation items list.
        /// </summary>
        public IList<NavMenuItem> NavigationItems { get; private set; }

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
        /// <param name="navigationItems">The navigation items.</param>
        /// <param name="bottomDockedNavigationItems">The optional navigation items that are docked at the bottom.</param>
        public AppShell(Frame frame, IEnumerable<NavMenuItem> navigationItems, IEnumerable<NavMenuItem> bottomDockedNavigationItems = null)
        {
            InitializeComponent();
            RootSplitView.Content = frame;
            _rootFrame = frame;

            _rootFrame.Navigated += OnNavigatedToPage;

            Loaded += (sender, args) =>
            {
                Current = this;

                SelectNavigationItem(NavigationService.CurrentPageType);

                TogglePaneButton.Focus(FocusState.Programmatic);
            };

            SizeChanged += (s, e) =>
            {
                // update the button size when the frame size changes, due to possible changes of the adaptive UI
                CheckTogglePaneButtonSizeChanged();
            };

            if (navigationItems == null || navigationItems.Count() == 0)
                throw new ArgumentException("There must be at least one top-level navigation item. Did you forgot to override a method in UniversalApp?");

            NavigationItems = new List<NavMenuItem>(navigationItems);
            NavMenuList.ItemsSource = navigationItems;

            if (bottomDockedNavigationItems != null && bottomDockedNavigationItems.Count() > 0)
            {
                NavMenuListBottomDock.ItemsSource = bottomDockedNavigationItems;

                foreach (var item in bottomDockedNavigationItems)
                {
                    NavigationItems.Add(item);
                }
            }
            else
            {
                NavMenuSeperator.Visibility = Visibility.Collapsed;
            }
                
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

        /// <summary>
        /// Gest a hold of the current frame so that we can inspect the app back stack.
        /// </summary>
        /// <param name="handled">The handled flag.</param>
        public void BackRequested(ref bool handled)
        {
            if (AppFrame == null)
                return;

            if (RootSplitView.IsSwipeablePaneOpen &&
                RootSplitView.DisplayMode == SplitViewDisplayMode.Overlay || RootSplitView.DisplayMode == SplitViewDisplayMode.CompactOverlay)
            {
                handled = true;
                RootSplitView.CloseSwipeablePane();
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
                    var nav = UniversalApp.Current.NavigationService;

                    // navigate only to new pages
                    if (nav.CurrentPageType != null && nav.CurrentPageType != item.DestinationPage)
                        nav.Navigate(item.DestinationPage, item.Parameter);
                }
            }
        }

        /// <summary>
        /// Gets the container from the given navigation item, either from the main list or the bottom dock.
        /// </summary>
        /// <remarks>
        /// UpdateLayout is invoked to make sure the containers are available even when the ListView was not rendered.
        /// </remarks>
        /// <param name="item">The navigation menu item.</param>
        /// <returns>The list view item container.</returns>
        private ListViewItem GetContainerFromItem(NavMenuItem item)
        {
            NavMenuList.UpdateLayout();
            NavMenuList.ScrollIntoView(item);
            var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

            if (container == null)
            {
                NavMenuListBottomDock.UpdateLayout();
                container = (ListViewItem)NavMenuListBottomDock.ContainerFromItem(item);
            }   

            return container;
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            SelectNavigationItem(e.SourcePageType);

            // after a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }
        }

        private NavMenuItem SelectNavigationItem(NavMenuItem item)
        {
            if (item == null && AppFrame.BackStackDepth > 0)
            {
                // in cases where a page drills into sub-pages then we'll highlight the most recent
                // navigation menu item that appears in the BackStack
                foreach (var entry in AppFrame.BackStack.Reverse())
                {
                    item = GetNavigationItem(entry.SourcePageType);
                    if (item != null)
                        break;
                }
            }
            else if (item == null)
            {
                var page = AppFrame.Content as UniversalPage;
                if (page != null)
                    item = GetNavigationItem(page.ParentPage);
            }

            var container = GetContainerFromItem(item);

            // while updating the selection state of the item prevent it from taking keyboard focus.  If a
            // user is invoking the back button via the keyboard causing the selected nav menu item to change
            // then focus will remain on the back button.
            if (container != null)
                container.IsTabStop = false;

            NavMenuList.SetSelectedItem(container);
            NavMenuListBottomDock.SetSelectedItem(container);

            if (container != null)
                container.IsTabStop = true;
            return item;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
            CheckTogglePaneButtonSizeChanged();
        }
        
        private void SelectNavigationItem(Type pageType)
        {
            // select the current item
            var item = GetNavigationItem(pageType);
            SelectNavigationItem(item);
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
        /// Sets the (optional) colors of the AppShell Hamburger menu.
        /// </summary>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        public void SetToggleButtonColors(Color? foreground, Color? background)
        {
            if (foreground.HasValue)
                TogglePaneButton.Foreground = new SolidColorBrush(foreground.Value);
            if (background.HasValue)
            {
                HamburgerButtonBackground.Fill = new SolidColorBrush(background.Value);
            }
            else
            {
                HamburgerButtonBackground.Fill = new SolidColorBrush(Colors.Transparent);
            }   
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
        private void TogglePaneButton_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckTogglePaneButtonSizeChanged();
        }

        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            // update selected item when toggle changes (also happens when split view opens after windows resize)
            var item = GetNavigationItem(_rootFrame.CurrentSourcePageType);
            var container = GetContainerFromItem(item);
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
                handler.DynamicInvoke(this, TogglePaneButtonRect);
            }
        }

        /// <summary>
        /// Gets the navigation item of the given page type.
        /// </summary>
        /// <param name="pageType">The page type.</param>
        /// <returns>Returns the navigation item.</returns>
        private NavMenuItem GetNavigationItem(Type pageType)
        {
            return (from p in NavigationItems where p.DestinationPage == pageType select p).FirstOrDefault();
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
