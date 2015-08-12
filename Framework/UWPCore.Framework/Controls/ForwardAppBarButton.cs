using System;
using UWPCore.Framework.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The FORWARD button of the app bar.
    /// </summary>
    public sealed class ForwardAppBarButton : AppBarButton
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
        /// Creates a ForwardAppBarButton instance.
        /// </summary>
        public ForwardAppBarButton()
        {
            Label = "Forward";
            Content = new FontIcon
            {
                FontFamily = SystemIcons.Font,
                Glyph = SystemIcons.ArrowRight
            };

            DefaultStyleKey = typeof(AppBarButton);
            Loaded += (s, e) =>
            {
                if (Frame == null) { throw new NullReferenceException("Please set Frame property"); }
                Visibility = CalculateOnCanvasBackVisibility();
            };
            RenderTransform = new ScaleTransform { ScaleX = -1 };
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
