using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// A simple page header class which includes a back and forward button.
    /// </summary>
    public sealed partial class PageHeader : UserControl
    {
        /// <summary>
        /// Creates a PageHeader instance.
        /// </summary>
        public PageHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// The Text dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
                typeof(PageHeader), new PropertyMetadata("Page Header"));

        /// <summary>
        /// Gets or sets whether the BACK button is visible.
        /// </summary>
        public bool ShowBack
        {
            get { return (bool)GetValue(ShowBackProperty); }
            set { SetValue(ShowBackProperty, value); }
        }

        /// <summary>
        /// The ShowBack dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowBackProperty =
            DependencyProperty.Register("ShowBack", typeof(bool),
                typeof(PageHeader), new PropertyMetadata(true, ShowBackChanged));

        /// <summary>
        /// The show back changed event handler.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The changed event args.</param>
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

        /// <summary>
        /// Gets or sets whether the FORWARD button is visible.
        /// </summary>
        public bool ShowForward
        {
            get { return (bool)GetValue(ShowForwardProperty); }
            set { SetValue(ShowForwardProperty, value); }
        }

        /// <summary>
        /// The ShowForward dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowForwardProperty =
            DependencyProperty.Register("ShowForward", typeof(bool),
                typeof(PageHeader), new PropertyMetadata(true, ShowForwardChanged));

        /// <summary>
        /// The show forward changed event handler.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The changed event args.</param>
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
