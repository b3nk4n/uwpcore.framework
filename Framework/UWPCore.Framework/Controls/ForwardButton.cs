using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The FORWARD button control.
    /// </summary>
    public sealed class ForwardButton : Button
    {
        /// <summary>
        /// The visibility changed event.
        /// </summary>
        public event EventHandler VisibilityChanged;

        /// <summary>
        /// Gets or sets the frame.
        /// </summary>
        public Frame Frame { get; set; }

        /// <summary>
        /// Creates a ForwardButton instance.
        /// </summary>
        public ForwardButton()
        {
            RenderTransform = new ScaleTransform { ScaleX = -1 };
            Style = Resources["NavigationBackButtonNormalStyle"] as Style;
            DefaultStyleKey = typeof(ForwardButton);
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
            Click += (s, e) => Frame.GoForward();
            Window.Current.SizeChanged += (s, arg) => Visibility = CalculateOnCanvasBackVisibility();
            RegisterPropertyChangedCallback(VisibilityProperty, (s, e) => VisibilityChanged?.Invoke(this, EventArgs.Empty));
        }

        /// <summary>
        /// Calculate the FORWARD visibilty on the canvas.
        /// </summary>
        /// <returns>Returns the visiblity of the FORWARD button.</returns>
        private Visibility CalculateOnCanvasBackVisibility()
        {
            // by design it is not visible when not applicable
            var cangoforward = Frame.CanGoForward;
            if (!cangoforward)
                return Visibility.Collapsed;

            // at this point, we show the on-canvas button
            return Visibility.Visible;
        }
    }
}
