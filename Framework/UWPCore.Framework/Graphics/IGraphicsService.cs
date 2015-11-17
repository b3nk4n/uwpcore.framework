using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPCore.Framework.Graphics
{
    /// <summary>
    /// The service interface for rendering of images.
    /// </summary>
    public interface IGraphicsService
    {
        /// <summary>
        /// Renders the UIElement to the file.
        /// </summary>
        /// <param name="uiElement">The UIElement to render.</param>
        /// <param name="file">The file to render to.</param>
        /// <param name="bitmapEncoder">The bitmap encoder GUID, such as BitmapEncoder.PngEncoderId.</param>
        /// <returns>The RenderTargetBitmap containing the rendered data.</returns>
        Task<RenderTargetBitmap> RenderToFileAsync(UIElement uiElement, IStorageFile file, Guid bitmapEncoder);

        /// <summary>
        /// Renders the UIElement to the stream.
        /// </summary>
        /// <param name="uiElement">The UIElement to render.</param>
        /// <param name="stream">The file stream to render to.</param>
        /// <param name="bitmapEncoder">The bitmap encoder GUID, such as BitmapEncoder.PngEncoderId.</param>
        /// <returns>The RenderTargetBitmap containing the rendered data.</returns>
        Task<RenderTargetBitmap> RenderToStreamAsync(UIElement uiElement, IRandomAccessStream stream, Guid bitmapEncoder);

        /// <summary>
        /// Resizes the image file and saves the resized image in the destination file.
        /// </summary>
        /// <param name="sourceFile">The source image file.</param>
        /// <param name="destinationFile">The destination image file.</param>
        /// <param name="maxWidth">The max image width.</param>
        /// <param name="maxHeight">The max image heigth.</param>
        /// <param name="dpi">The destination image DPIs.</param>
        /// <returns>Returns True for success, else False.</returns>
        Task<bool> ResizeImageAsync(IStorageFile sourceFile, IStorageFile destinationFile, uint maxWidth, uint maxHeight, uint dpi = 96);
    }
}
