using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Models
{
    /// <summary>
    /// The contributor model class for the about page.
    /// </summary>
    public sealed class ContributorModel : BindableBase
    {
        /// <summary>
        /// The contributor title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The contributor icon.
        /// </summary>
        public ImageSource Icon { get; set; }
    }
}
