using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace UWPCore.Framework.Store
{
    /// <summary>
    /// The lincense service class to get license information or do in-app-purchasing.
    /// </summary>
    public class LicenseService : ILicenseService
    {
        /// <summary>
        /// The license information, eigher simulated or live depending on solution config..
        /// </summary>
        private static LicenseInformation _licenseInfo;

        /// <summary>
        /// Creates a LicenseServce instance.
        /// </summary>
        public LicenseService()
        {
#if DEBUG
            _licenseInfo = CurrentAppSimulator.LicenseInformation;
#else
            _licenseInfo = CurrentApp.LicenseInformation;
#endif
        }

        public bool IsProductActive(string productId)
        {
            try
            {
                return _licenseInfo.ProductLicenses[productId].IsActive;
            }
            catch (Exception e)
            {
                // BugSense: Exception from HRESULT: 0xD0000022
                Debug.WriteLine("Checking product licence failed with error: " + e.Message);
                return false;
            }
        }

        public async Task<bool> RequestProductPurchaseAsync(string productId)
        {
            if (IsProductActive(productId))
                return false;
            try
            {
#if DEBUG
                await CurrentAppSimulator.RequestProductPurchaseAsync(productId);
#else
                await CurrentApp.RequestProductPurchaseAsync(productId, false);
#endif
                return true;
            }
            catch (Exception)
            {
                // thrown when the user cancels the pruchase...
                return false;
            }
        }

        public async Task<IList<ProductItem>> LoadProductsAsync(IEnumerable<string> supportedProductIds, string localizedPurchasedText = null)
        {
            var productItems = new List<ProductItem>();

            // fallback purchased text
            if (string.IsNullOrEmpty(localizedPurchasedText))
                localizedPurchasedText = "purchased";

            try
            {
                // load supported products
#if DEBUG
                ListingInformation lisitingInfo = await CurrentAppSimulator.LoadListingInformationByProductIdsAsync(supportedProductIds);
#else
                ListingInformation lisitingInfo = await CurrentApp.LoadListingInformationByProductIdsAsync(supportedProductIds);
#endif
                foreach (var id in lisitingInfo.ProductListings.Keys)
                {
                    ProductListing product = lisitingInfo.ProductListings[id];
                    var isProductActive = IsProductActive(id);
                    var status = isProductActive ? localizedPurchasedText : product.FormattedPrice;
                    var imageLink = string.Empty;
                    productItems.Add(
                        new ProductItem
                        {
                            ImageUri = product.ImageUri,
                            Name = product.Name,
                            Description = product.Description,
                            Status = status,
                            Id = id,
                            IsActive = isProductActive
                        }
                    );
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Loading of products failed with error: " + e.Message);
            }

            return productItems;
        }

        public bool IsTrial
        {
            get
            {
                return _licenseInfo.IsTrial;
            }
        }
    }
}
