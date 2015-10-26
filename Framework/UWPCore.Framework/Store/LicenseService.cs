using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
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
            // TODO: check simulator requirements: https://social.msdn.microsoft.com/Forums/office/en-US/0298c819-732e-47a3-99a1-1bfac3e245c8/access-denied-on-currentappsimulatorlicenseinformation?forum=wpdevelop
            //_licenseInfo = CurrentAppSimulator.LicenseInformation;
            _licenseInfo = CurrentApp.LicenseInformation;
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
                Logger.WriteLine(e, "Checking product licence failed");
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
                var result = await CurrentAppSimulator.RequestProductPurchaseAsync(productId);
#else
                var result = await CurrentApp.RequestProductPurchaseAsync(productId);
#endif
                return result.Status == ProductPurchaseStatus.Succeeded || result.Status == ProductPurchaseStatus.AlreadyPurchased;
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
                Logger.WriteLine(e, "Loading of products failed");
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
