using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// A simple page header class which includes a back and forward button.
    /// </summary>
    public sealed partial class PageHeader : UserControl
    {
        public PageHeader()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
                typeof(PageHeader), new PropertyMetadata("Page Header"));

        public bool ShowBack
        {
            get { return (bool)GetValue(ShowBackProperty); }
            set { SetValue(ShowBackProperty, value); }
        }
        public static readonly DependencyProperty ShowBackProperty =
            DependencyProperty.Register("ShowBack", typeof(bool),
                typeof(PageHeader), new PropertyMetadata(true, ShowBackChanged));
        private static void ShowBackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as PageHeader;
            var g = p.Content as Grid;
            if ((bool)e.NewValue)
            {
                g.ColumnDefinitions[0].Width = GridLength.Auto;
            }
            else
            {
                g.ColumnDefinitions[0].Width = new GridLength(0);
            }
        }

        public bool ShowForward
        {
            get { return (bool)GetValue(ShowForwardProperty); }
            set { SetValue(ShowForwardProperty, value); }
        }
        public static readonly DependencyProperty ShowForwardProperty =
            DependencyProperty.Register("ShowForward", typeof(bool),
                typeof(PageHeader), new PropertyMetadata(true, ShowForwardChanged));
        private static void ShowForwardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as PageHeader;
            var g = p.Content as Grid;
            if ((bool)e.NewValue)
            {
                g.ColumnDefinitions[2].Width = GridLength.Auto;
            }
            else
            {
                g.ColumnDefinitions[2].Width = new GridLength(0);
            }
        }
    }
}
