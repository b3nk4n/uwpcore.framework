using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UWPCore.Framework.Logging;
using UWPCore.Framework.Storage;
using Windows.ApplicationModel.Store;
using Windows.Storage;

namespace UWPCore.Framework.Store
{
    /// <summary>
    /// The lincense service class to get license information or do in-app-purchasing.
    /// </summary>
    public class LicenseService : ILicenseService
    {
        private IStorageService _localStorageService;

        /// <summary>
        /// Creates a LicenseServce instance.
        /// </summary>
        [Inject]
        public LicenseService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public virtual bool IsProductActive(string productId)
        {
            try
            {
                return GetLicenseInformation().ProductLicenses[productId].IsActive;
            }
            catch (Exception e)
            {
                // BugSense: Exception from HRESULT: 0xD0000022
                Logger.WriteLine(e, "Checking product licence failed");
                return false;
            }
        }

        public virtual async Task<bool> RequestProductPurchaseAsync(string productId)
        {
            if (IsProductActive(productId))
                return false; // TODO: return TRUE here?
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
                return null;
            }

            return productItems;
        }

        public bool IsTrial
        {
            get
            {
                return GetLicenseInformation().IsTrial;
            }
        }

        /// <summary>
        /// Refreshes the simulator.
        /// </summary>
        /// <returns></returns>
#pragma warning disable 1998
        public async Task RefeshSimulator()
        {
#if DEBUG
            var proxyPath = IOConstants.APPX_SCHEME + "/Assets/IAP/WindowsStoreProxy.xml";
            var proxyFile = await _localStorageService.GetFileFromApplicationAsync(proxyPath);

            if (proxyFile != null)
            {
                await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile as StorageFile);
            }  
            else
            {
                throw new FileNotFoundException("Could not find the proxy file under the path: " + proxyPath);
            }
#endif
        }
#pragma warning restore 1998

        /// <summary>
        /// Gets the appropriate license information.
        /// </summary>
        /// <returns>Either the real or limulated license information.</returns>
        private LicenseInformation GetLicenseInformation()
        {
#if DEBUG
            return CurrentAppSimulator.LicenseInformation;
#else
            return CurrentApp.LicenseInformation;
#endif
        }
    }
}
