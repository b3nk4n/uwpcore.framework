using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWPCore.Framework.Store
{
    /// <summary>
    /// The interface to get license information or do in-app-purchasing.
    /// </summary>
    public interface ILicenseService
    {
        /// <summary>
        /// Verifies whether the product is active/purchased.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>
        /// Returns true if the product is active and has been purchased, else false.
        /// </returns>
        bool IsProductActive(string productId);

        /// <summary>
        /// Request a product purchase.
        /// </summary>
        /// <param name="productId">The product ID to purchase.</param>
        /// <returns>
        /// Returns true if the purchase was successful, and false
        /// if the purchase was unsuccessful or the product has already been purchased.
        /// </returns>
        Task<bool> RequestProductPurchaseAsync(string productId);

        /// <summary>
        /// Loads the products asynchronously.
        /// </summary>
        /// <param name="supportedProductIds">The supporeted in-app product IDs.</param>
        /// <param name="localizedPurchasedText">The localized purchased text.</param>
        /// <returns></returns>
        Task<IList<ProductItem>> LoadProductsAsync(IEnumerable<string> supportedProductIds, string localizedPurchasedText = null);

        /// <summary>
        /// Refreshes the simulator. This method has only an effec on debug mode.
        /// This method should be called in <see cref="Common.UniversalApp.OnInitializeAsync"/>.
        /// </summary>
        Task RefeshSimulator();

        /// <summary>
        /// Gets whether the app runs on trial mode or not.
        /// </summary>
        bool IsTrial { get; }
    }
}
