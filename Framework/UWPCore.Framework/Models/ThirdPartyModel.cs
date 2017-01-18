using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Models
{
    public sealed class ThirdPartyModel : BindableBase
    {
        /// <summary>
        /// The 3rd party title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The 3rd party icon.
        /// </summary>
        public ImageSource Icon { get; set; }

        /// <summary>
        /// The 3rd party license.
        /// </summary>
        public string License{ get; set; }

        /// <summary>
        /// The 3rd party license.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The 3rd party license.
        /// </summary>
        public string Link { get; set; }
    }
}
