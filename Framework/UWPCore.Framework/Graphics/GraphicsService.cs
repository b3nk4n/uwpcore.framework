using System;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore.Framework.Logging;
using Windows.Storage;

namespace UWPCore.Framework.Graphics
{
    /// <summary>
    /// The graphics service for rendering images at runtime.
    /// <remarks>
    /// Inspired by: http://mariusbancila.ro/blog/2013/11/05/render-the-screen-of-a-windows-store-app-to-a-bitmap-in-windows-8-1/
    /// Note:
    /// For rendering an image in a background task, use XamlRenderingBackgroundTask:
    /// <see cref="https://social.msdn.microsoft.com/Forums/en-US/43295c90-43e8-4b08-8a25-958a1c3d0a0b/explanation-on-windowsuixamlmediaxamlrenderingbackgroundtask?forum=WindowsPhonePreviewSDK"/>
    /// </remarks>
    /// </summary>
    public class GraphicsService : IGraphicsService
    {
        public async Task<RenderTargetBitmap> RenderToFileAsync(UIElement uiElement, IStorageFile file, Guid bitmapEncoder)
        {
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                return await RenderToStreamAsync(uiElement, stream, bitmapEncoder);
            }
        }

        public async Task<RenderTargetBitmap> RenderToStreamAsync(UIElement uiElement, IRandomAccessStream stream, Guid bitmapEncoder)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uiElement);

                var pixels = await renderTargetBitmap.GetPixelsAsync();

                var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                var encoder = await BitmapEncoder.CreateAsync(bitmapEncoder, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    logicalDpi,
                    logicalDpi,
                    pixels.ToArray());

                await encoder.FlushAsync();

                return renderTargetBitmap;
            }
            catch (Exception e)
            {
                Logger.WriteLine(e, "Rendering image failed");
                return null;
            }
        }

        /// <summary>
        /// Gets the bitmap encoder of the given file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The corresponding bitmap encoder.</returns>
        private Guid GetBitmapEncoder(string fileExtension)
        {
            Guid encoderId = BitmapEncoder.JpegEncoderId;
            switch (fileExtension)
            {
                case ".bmp":
                    encoderId = BitmapEncoder.BmpEncoderId;
                    break;
                case ".gif":
                    encoderId = BitmapEncoder.GifEncoderId;
                    break;
                case ".png":
                    encoderId = BitmapEncoder.PngEncoderId;
                    break;
                case ".tif":
                    encoderId = BitmapEncoder.TiffEncoderId;
                    break;
            }

            return encoderId;
        }
    }

}
