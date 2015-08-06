using System;
using UWPCore.Framework.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Controls
{
    public sealed class ForwardAppBarButton : AppBarButton
    {
        public event EventHandler VisibilityChanged;

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

        public Frame Frame { get; set; }

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
