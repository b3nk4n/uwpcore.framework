using Windows.UI.Xaml.Controls;

namespace UWPCore.Demo.Controls
{
    /// <summary>
    /// Simple tile square medium template.
    /// </summary>
    public sealed partial class TileSquareMedium : UserControl
    {
        /// <summary>
        /// Create a TileSquareMedium instance.
        /// </summary>
        public TileSquareMedium()
            : this("Title", "Subtitle")
        {
        }

        /// <summary>
        /// Create a TileSquareMedium instance.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="subtitle">The subtitle.</param>
        public TileSquareMedium(string title, string subtitle)
        {
            InitializeComponent();

            TitleTextBox.Text = title;
            SubtitleTextBox.Text = subtitle;
        }
    }
}
