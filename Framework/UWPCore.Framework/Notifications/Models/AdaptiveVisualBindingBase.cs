using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Notifications.Models
{
    /// <summary>
    /// The branding type for visual elements.
    /// </summary>
    public enum VisualBranding
    {
        None,
        Logo,
        Name,
        NameAndLogo
    }

    /// <summary>
    /// The base class for adaptive visual binding and visual elements.
    /// </summary>
    public abstract class AdaptiveVisualBindingBase
    {
        /// <summary>
        /// Gets or sets the optional language.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Gets or sets the optional base URI.
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the optional branding type.
        /// </summary>
        public VisualBranding? Branding { get; set; }

        /// <summary>
        /// Gets or sets the optinal flag that indicates whether to add image query.
        /// </summary>
        public bool? AddImageQuery { get; set; }

        /// <summary>
        /// Gets or sets the optional content ID.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the optional display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the XML attributes.
        /// </summary>
        /// <returns>The XML attributes.</returns>
        protected List<XAttribute> GetXAttributes()
        {
            var attributes = new List<XAttribute>();
            if (!string.IsNullOrWhiteSpace(Lang))
            {
                attributes.Add(new XAttribute("lang", Lang));
            }

            if (BaseUri != null)
            {
                attributes.Add(new XAttribute("baseUri", BaseUri.ToString()));
            }

            if (Branding.HasValue)
            {
                attributes.Add(new XAttribute("branding", Branding.Value.ToString().FirstLetterToLower()));
            }

            if (AddImageQuery.HasValue)
            {
                attributes.Add(new XAttribute("addImageQuery", AddImageQuery.Value));
            }

            if (!string.IsNullOrWhiteSpace(ContentId))
            {
                attributes.Add(new XAttribute("contentId", ContentId));
            }

            if (!string.IsNullOrWhiteSpace(DisplayName))
            {
                attributes.Add(new XAttribute("displayName", DisplayName));
            }

            return attributes;
        }
    }
}
