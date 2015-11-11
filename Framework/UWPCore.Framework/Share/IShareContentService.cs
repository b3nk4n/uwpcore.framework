using System;
using Windows.Storage;

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

        /// <summary>
        /// Shares a image file.
        /// </summary>
        /// <param name="title">Te title.</param>
        /// <param name="imageFile">The image file to share.</param>
        /// <param name="thumbnailFile">The optional thumbnail file that is recommended by MSDN.</param>
        /// <param name="text">The optional content text to share.</param>
        /// <param name="description">The optional description.</param>
        void ShareImage(string title, IStorageFile imageFile, IStorageFile thumbnailFile = null, string text = null, string description = "");
    }
}
