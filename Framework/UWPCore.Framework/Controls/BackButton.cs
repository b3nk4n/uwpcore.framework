using System;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Controls
{
    public sealed class BackButton : Button
    {
        public event EventHandler VisibilityChanged;

        public BackButton()
        {
            Style = Resources["NavigationBackButtonNormalStyle"] as Style;
            DefaultStyleKey = typeof(BackButton);
            Loaded += (s, e) =>
            {
                DependencyObject item = this;
                while (!((item = VisualTreeHelper.GetParent(item)) is Page)) { }
                Page page = item as Page;
                Frame = page.Frame;
                Visibility = CalculateOnCanvasBackVisibility();
            };
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Visibility = Visibility.Visible;
                return;
            }
            Click += (s, e) => Frame.GoBack();
            Window.Current.SizeChanged += (s, arg) => Visibility = CalculateOnCanvasBackVisibility();
            RegisterPropertyChangedCallback(VisibilityProperty, (s, e) => VisibilityChanged?.Invoke(this, EventArgs.Empty));
        }

        public Frame Frame { get; private set; }

        private Visibility CalculateOnCanvasBackVisibility()
        {
            // by design it is not visible when not applicable
            var cangoback = Frame.CanGoBack;
            if (!cangoback)
                return Visibility.Collapsed;

            // mobile always has a visible back button
            var mobilefam = ResourceContext.GetForCurrentView().QualifierValues["DeviceFamily"].Equals("Mobile");
            if (mobilefam)
                return Visibility.Collapsed;

            // simply don't know what to do with else
            var desktopfam = ResourceContext.GetForCurrentView().QualifierValues["DeviceFamily"].Equals("Desktop");
            if (!desktopfam)
                return Visibility.Collapsed;

            // touch always has a bisible back button
            var touchmode = UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;
            if (touchmode)
                return Visibility.Collapsed;

            // full screen back button is only visible when mouse reveals title (prefered behavior is on-canvas visible)
            var fullscreen = ApplicationView.GetForCurrentView().IsFullScreen;
            if (fullscreen)
                return Visibility.Visible;

            // hide the button if the shell button is visible
            var optinback = SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility.Equals(AppViewBackButtonVisibility.Visible);
            if (optinback)
            {
                // shell button will not be visible if there is no title bar
                var hastitle = !CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar;
                if (hastitle)
                    return Visibility.Collapsed;
            }

            // at this point, we show the on-canvas button
            return Visibility.Visible;
        }
    }
}
