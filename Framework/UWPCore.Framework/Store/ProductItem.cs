using System;

namespace UWPCore.Framework.Store
{
    /// <summary>
    /// Represents a product item of an in-app purchase.
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        /// The 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the procuct.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the purchasing status.
        /// <remarks>
        /// Eighter the formatted price, or the localized 'purchased' text.
        /// </remarks>
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the image URI.
        /// </summary>
        public Uri ImageUri { get; set; }

        /// <summary>
        /// Gets or sets whether the product has been pruchased.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
