using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// Swipeable SplitView, taken from Justin Xin Liu.
    /// 1. This SplitView project was initially created based on Koen's awesome UWP templates.
    /// 2. Don't use IsPaneOpen, use IsSwipeablePaneOpen instead.
    /// 3. At the moment the swipe only works when DisplayMode is Overlay.
    /// 4. Currently only left edge is supported.
    /// 5. The code is not fully tested, let me know if you find any bugs.
    /// </summary>
    /// <see cref="https://github.com/JustinXinLiu/SwipeableSplitView"/>
    public sealed class SwipeableSplitView : SplitView
    {
        public const double SWIPE_REGION_LEFT = 16.0;

        #region private variables

        Grid _paneRoot;
        Grid _overlayRoot;
        Rectangle _panArea;
        Rectangle _dismissLayer;
        CompositeTransform _paneRootTransform;
        CompositeTransform _panAreaTransform;
        Storyboard _openSwipeablePane;
        Storyboard _closeSwipeablePane;

        #endregion

        public SwipeableSplitView()
        {
            this.DefaultStyleKey = typeof(SwipeableSplitView);
        }

        #region properties

        // safely subscribe/unsubscribe manipulation events here
        internal Grid PaneRoot
        {
            get { return _paneRoot; }
            set
            {
                if (_paneRoot != null)
                {
                    _paneRoot.ManipulationStarted -= OnManipulationStarted;
                    _paneRoot.ManipulationDelta -= OnManipulationDelta;
                    _paneRoot.ManipulationCompleted -= OnManipulationCompleted;
                }

                _paneRoot = value;

                if (_paneRoot != null)
                {
                    _paneRoot.ManipulationStarted += OnManipulationStarted;
                    _paneRoot.ManipulationDelta += OnManipulationDelta;
                    _paneRoot.ManipulationCompleted += OnManipulationCompleted;
                }
            }
        }

        // safely subscribe/unsubscribe manipulation events here
        internal Rectangle PanArea
        {
            get { return _panArea; }
            set
            {
                if (_panArea != null)
                {
                    _panArea.ManipulationStarted -= OnManipulationStarted;
                    _panArea.ManipulationDelta -= OnManipulationDelta;
                    _panArea.ManipulationCompleted -= OnManipulationCompleted;
                    _panArea.Tapped -= OnDismissLayerTapped;
                }

                _panArea = value;

                if (_panArea != null)
                {
                    _panArea.ManipulationStarted += OnManipulationStarted;
                    _panArea.ManipulationDelta += OnManipulationDelta;
                    _panArea.ManipulationCompleted += OnManipulationCompleted;
                    _panArea.Tapped += OnDismissLayerTapped;
                }
            }
        }

        // safely subscribe/unsubscribe manipulation events here
        internal Rectangle DismissLayer
        {
            get { return _dismissLayer; }
            set
            {
                if (_dismissLayer != null)
                {
                    _dismissLayer.Tapped -= OnDismissLayerTapped;
                }

                _dismissLayer = value;

                if (_dismissLayer != null)
                {
                    _dismissLayer.Tapped += OnDismissLayerTapped; ;
                }
            }
        }

        // safely subscribe/unsubscribe animation completed events here
        internal Storyboard OpenSwipeablePaneAnimation
        {
            get { return _openSwipeablePane; }
            set
            {
                if (_openSwipeablePane != null)
                {
                    _openSwipeablePane.Completed -= OnOpenSwipeablePaneCompleted;
                }

                _openSwipeablePane = value;

                if (_openSwipeablePane != null)
                {
                    _openSwipeablePane.Completed += OnOpenSwipeablePaneCompleted;
                }
            }
        }

        // safely subscribe/unsubscribe animation completed events here
        internal Storyboard CloseSwipeablePaneAnimation
        {
            get { return _closeSwipeablePane; }
            set
            {
                if (_closeSwipeablePane != null)
                {
                    _closeSwipeablePane.Completed -= OnCloseSwipeablePaneCompleted;
                }

                _closeSwipeablePane = value;

                if (_closeSwipeablePane != null)
                {
                    _closeSwipeablePane.Completed += OnCloseSwipeablePaneCompleted;
                }
            }
        }

        public bool IsSwipeablePaneOpen
        {
            get { return (bool)GetValue(IsSwipeablePaneOpenProperty); }
            set
            {
                SetValue(IsSwipeablePaneOpenProperty, value);
            }
        }

        public static readonly DependencyProperty IsSwipeablePaneOpenProperty =
            DependencyProperty.Register("IsSwipeablePaneOpen", typeof(bool), typeof(SwipeableSplitView), new PropertyMetadata(false, OnIsSwipeablePaneOpenChanged));

        static void OnIsSwipeablePaneOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var splitView = (SwipeableSplitView)d;

            switch (splitView.DisplayMode)
            {
                case SplitViewDisplayMode.Inline:
                case SplitViewDisplayMode.CompactOverlay:
                case SplitViewDisplayMode.CompactInline:
                    splitView.IsPaneOpen = (bool)e.NewValue;
                    break;

                case SplitViewDisplayMode.Overlay:
                    if (splitView.OpenSwipeablePaneAnimation == null || splitView.CloseSwipeablePaneAnimation == null)
                        return;

                    if ((bool)e.NewValue)
                    {
                        splitView.OpenSwipeablePane();
                    }
                    else
                    {
                        splitView.CloseSwipeablePane();
                    }
                    break;
            }
        }

        public double PanAreaInitialTranslateX
        {
            get { return (double)GetValue(PanAreaInitialTranslateXProperty); }
            set { SetValue(PanAreaInitialTranslateXProperty, value); }
        }

        public static readonly DependencyProperty PanAreaInitialTranslateXProperty =
            DependencyProperty.Register("PanAreaInitialTranslateX", typeof(double), typeof(SwipeableSplitView), new PropertyMetadata(0d));

        public double PanAreaThreshold
        {
            get { return (double)GetValue(PanAreaThresholdProperty); }
            set { SetValue(PanAreaThresholdProperty, value); }
        }

        public static readonly DependencyProperty PanAreaThresholdProperty =
            DependencyProperty.Register("PanAreaThreshold", typeof(double), typeof(SwipeableSplitView), new PropertyMetadata(36d));


        /// <summary>
        /// enabling this will allow users to select a menu item by panning up/down on the bottom area of the left pane,
        /// this could be particularly helpful when holding large phones since users don't need to stretch their fingers to
        /// reach the top part of the screen to select a different menu item.
        /// </summary>
        public bool IsPanSelectorEnabled
        {
            get { return (bool)GetValue(IsPanSelectorEnabledProperty); }
            set { SetValue(IsPanSelectorEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsPanSelectorEnabledProperty =
            DependencyProperty.Register("IsPanSelectorEnabled", typeof(bool), typeof(SwipeableSplitView), new PropertyMetadata(true));

        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PaneRoot = GetTemplateChild("PaneRoot") as Grid;
            _overlayRoot = GetTemplateChild("OverlayRoot") as Grid;
            PanArea = GetTemplateChild("PanArea") as Rectangle;
            DismissLayer = GetTemplateChild("DismissLayer") as Rectangle;

            if (PaneRoot == null || _overlayRoot == null || PanArea == null || DismissLayer == null)
            {
                throw new ArgumentException("Make sure you have copied the default style to Generic.xaml!!");
            }

            var rootGrid = _paneRoot.Parent as Grid;

            if (rootGrid == null)
            {
                throw new ArgumentException("Make sure you have copied the default style to Generic.xaml!!");
            }

            OpenSwipeablePaneAnimation = rootGrid.Resources["OpenSwipeablePane"] as Storyboard;
            CloseSwipeablePaneAnimation = rootGrid.Resources["CloseSwipeablePane"] as Storyboard;

            if (OpenSwipeablePaneAnimation == null || CloseSwipeablePaneAnimation == null)
            {
                throw new ArgumentException("Make sure you have copied the default style to Generic.xaml!!");
            }

            // initialization
            OnDisplayModeChanged(null, null);

            RegisterPropertyChangedCallback(DisplayModeProperty, OnDisplayModeChanged);

            // disable ScrollViewer as it will prevent finger from panning
            if (Pane is ListView || Pane is ListBox)
            {
                ScrollViewer.SetVerticalScrollMode(Pane, ScrollMode.Disabled);
            }
        }

        #region native property change handlers

        void OnDisplayModeChanged(DependencyObject sender, DependencyProperty dp)
        {
            switch (DisplayMode)
            {
                case SplitViewDisplayMode.Inline:
                case SplitViewDisplayMode.CompactOverlay:
                case SplitViewDisplayMode.CompactInline:
                    PanAreaInitialTranslateX = 0d;
                    _overlayRoot.Visibility = Visibility.Collapsed;
                    break;

                case SplitViewDisplayMode.Overlay:
                    PanAreaInitialTranslateX = OpenPaneLength * -1;
                    _overlayRoot.Visibility = Visibility.Visible;
                    break;
            }

            ((CompositeTransform)_paneRoot.RenderTransform).TranslateX = PanAreaInitialTranslateX;
        }

        #endregion

        #region manipulation event handlers

        void OnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (DisplayMode == SplitViewDisplayMode.Overlay &&
                !IsSwipeablePaneOpen &&
                e.Position.X > SWIPE_REGION_LEFT)
                return;

            _panAreaTransform = PanArea.RenderTransform as CompositeTransform;
            _paneRootTransform = PaneRoot.RenderTransform as CompositeTransform;

            if (_panAreaTransform == null || _paneRootTransform == null)
            {
                throw new ArgumentException("Make sure you have copied the default style to Generic.xaml!!");
            }
        }

        void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (_panAreaTransform == null || _paneRootTransform == null)
                return;

            var x = _panAreaTransform.TranslateX + e.Delta.Translation.X;

            // keep the pan within the bountry
            if (x < PanAreaInitialTranslateX || x > 0) return;

            // while we are panning the PanArea on X axis, let's sync the PaneRoot's position X too
            _paneRootTransform.TranslateX = _panAreaTransform.TranslateX = x;
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (_panAreaTransform == null || _paneRootTransform == null)
                return;

            var x = e.Velocities.Linear.X;

            // ignore a little bit velocity (+/-0.1) AND mini-swipes
            if (x <= -0.1 ||
                Math.Abs(e.Cumulative.Translation.X) < 20)
            {
                CloseSwipeablePane();
            }
            else if (x > -0.1 && x < 0.1)
            {
                if (Math.Abs(_panAreaTransform.TranslateX) > Math.Abs(PanAreaInitialTranslateX) / 2)
                {
                    CloseSwipeablePane();
                }
                else
                {
                    OpenSwipeablePane();
                }
            }
            else
            {
                OpenSwipeablePane();
            }

            _panAreaTransform = null;
            _paneRootTransform = null;
        }

        #endregion

        #region DismissLayer tap event handlers

        void OnDismissLayerTapped(object sender, TappedRoutedEventArgs e)
        {
            CloseSwipeablePane();
        }

        #endregion

        #region animation completed event handlers

        void OnOpenSwipeablePaneCompleted(object sender, object e)
        {
            this.DismissLayer.IsHitTestVisible = true;
        }

        void OnCloseSwipeablePaneCompleted(object sender, object e)
        {
            this.DismissLayer.IsHitTestVisible = false;
        }

        #endregion

        #region private methods

        public void OpenSwipeablePane()
        {
            if (IsSwipeablePaneOpen)
            {
                if (OpenSwipeablePaneAnimation != null)
                    OpenSwipeablePaneAnimation.Begin();
            }
            else
            {
                IsSwipeablePaneOpen = true;
            }
        }

        public async void CloseSwipeablePane()
        {
            if (!IsSwipeablePaneOpen)
            {
                // workaround: fixes the problem that the close animation is sometimes not played
                await Task.Delay(10);

                if (CloseSwipeablePaneAnimation != null)
                    CloseSwipeablePaneAnimation.Begin();
            }
            else
            {
                IsSwipeablePaneOpen = false;
            }
        }

        #endregion
    }
}
