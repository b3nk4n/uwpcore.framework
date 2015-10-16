using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// The default page header of a UWP app, that supports nested titles or command bars.
    /// </summary>
    public sealed partial class PageHeader : UserControl
    {
        /// <summary>
        /// Creates a PageHeader instance.
        /// </summary>
        public PageHeader()
        {
            InitializeComponent();

            Loaded += (s, a) =>
            {
                if (AppShell.Current != null)
                {
                    AppShell.Current.TogglePaneButtonRectChanged += Current_TogglePaneButtonSizeChanged;
                    titleBar.Margin = new Thickness(AppShell.Current.TogglePaneButtonRect.Right, 0, 0, 0);
                }
            };
        }

        private void Current_TogglePaneButtonSizeChanged(AppShell sender, Rect e)
        {
            titleBar.Margin = new Thickness(e.Right, 0, 0, 0);
        }

        /// <summary>
        /// Gets or sets the headers content.
        /// </summary>
        public UIElement HeaderContent
        {
            get { return (UIElement)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(UIElement), typeof(PageHeader), new PropertyMetadata(DependencyProperty.UnsetValue));
    }
}
