using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Launcher
{
    /// <summary>
    /// The Windows Store page category.
    /// </summary>
    public enum StoreTopLevelCategory
    {
        Apps,
        Games,
        Music,
        Video
    }

    /// <summary>
    /// The Windows Store app product categories.
    /// </summary>
    public enum StoreProductCategory
    {
        Productivity,
        Health,
        [Display(Name = "Health+%26+fitness")]
        HealthFitness,
        Social,
        Photos,
        Games,
        Security,
        Tools,
        [Display(Name = "Music+%26+videos")]
        MusicVideos,
        Entertainment,
        Finance,
        [Display(Name = "News+%26+weather")]
        NewsWeather,
        Shopping,
        [Display(Name = "Food+%26+dining")]
        FoodDining,
        Travel,
        Sports,
        Education,
        Lifestyle,
        Government,
        Business,
        [Display(Name = "Books+%26+reference")]
        BooksReference
    }

    /// <summary>
    /// Launcher class for Windows Store.
    /// </summary>
    /// <remarks>
    /// The URI scheme is documented under <see cref="https://msdn.microsoft.com/en-us/library/windows/apps/mt228343.aspx"/>.
    /// </remarks>
    public class StoreLauncher
    {
        /// <summary>
        /// Launches the Windows Store home page.
        /// </summary>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchHomeAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://home"));
        }

        /// <summary>
        /// Launches the Windows Store with the given category.
        /// </summary>
        /// <param name="category">The top level category.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchTopLevelCategoryAsync(StoreTopLevelCategory category)
        {
            var uriString = string.Format("ms-windows-store://navigatetopage/?Id={0}", category.ToString());
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the app in the Windows Store.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchAppAsync(string productId)
        {
            string uriString = string.Format("ms-windows-store://pdp/?ProductId={0}", productId);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store for review.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchReviewAsync(string productId)
        {
            string uriString = string.Format("ms-windows-store://review/?ProductId={0}", productId);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store for apps associated with a file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension, such as 'pdf'.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSearchAppsByFileExtensionAsync(string fileExtension)
        {
            string uriString = string.Format("ms-windows-store://assoc/?FileExt={0}", fileExtension);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store for apps associated with a protocol.
        /// </summary>
        /// <param name="protocol">The protocol, such as 'ms-word'.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSearchAppsByProtocolAsync(string protocol)
        {
            string uriString = string.Format("ms-windows-store://assoc/?Protocol={0}", protocol);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store for apps associated with a protocol.
        /// </summary>
        /// <param name="protocol">The protocol, such as 'ms-word'.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSearchAppsByTagsAsync(string[] tags)
        {
            var tagString = string.Join(",", tags);
            string uriString = string.Format("ms-windows-store://assoc/?Protocol={0}", tagString);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches a search in the Windows Store.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSearchAsync(string query)
        {
            var uriString = string.Format("ms-windows-store://search/?query={0}", query);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store with the given category.
        /// </summary>
        /// <param name="category">The product category.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchCategoryAsync(StoreProductCategory category)
        {
            var uriString = string.Format("ms-windows-store://browse/?type=Apps&cat={0}", category.GetDisplayValue());
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches a search for apps of a publisher in the Windows Store.
        /// </summary>
        /// <param name="publisherName">The publisher name.</param>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSearchAppsByPublisherAsync(string publisherName)
        {
            var uriString = string.Format("ms-windows-store://publisher/?name={0}", publisherName);
            return await Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }

        /// <summary>
        /// Launches the Windows Store downloads and updates page.
        /// </summary>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchDownloadAndUpdatesAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://downloadsandupdates"));
        }

        /// <summary>
        /// Launches the Windows Store settings page.
        /// </summary>
        /// <returns>Returns True if successful, else False.</returns>
        public static async Task<bool> LaunchSettingsAsync()
        {
            return await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://settings"));
        }
    }
}
