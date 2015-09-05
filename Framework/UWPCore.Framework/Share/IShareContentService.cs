using System;

namespace UWPCore.Framework.Share
{
    /// <summary>
    /// Service interface for services to share content.
    /// </summary>
    public interface IShareContentService
    {
        /// <summary>
        /// Shares a web link.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="link">The link to share.</param>
        /// <param name="description">The optional description.</param>
        void ShareWebLink(string title, Uri link, string description = "");

        /// <summary>
        /// Shares a text.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="text">The text to share.</param>
        /// <param name="description">The optional description.</param>
        void ShareText(string title, string text, string description = "");
    }
}
