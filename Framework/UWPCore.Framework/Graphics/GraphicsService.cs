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
                Logger.WriteLine(e, "Rendering image failed. Forgot to add the image to the visual tree?");
                return null;
            }
        }

        public async Task<bool> ResizeImageAsync(IStorageFile sourceFile, IStorageFile destinationFile, uint maxWidth, uint maxHeight, uint dpi = 96)
        {
            if (sourceFile == null || destinationFile == null)
                return false;

            try
            {
                using (var sourceStream = await sourceFile.OpenAsync(FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);

                    var inTransformation = GetTransformation(decoder.PixelWidth, decoder.PixelHeight, maxWidth, maxHeight);
                    var outTransformation = GetTransformation(decoder.OrientedPixelWidth, decoder.OrientedPixelHeight, maxWidth, maxHeight);

                    PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                        decoder.BitmapPixelFormat,
                        BitmapAlphaMode.Straight,
                        inTransformation,
                        ExifOrientationMode.RespectExifOrientation,
                        ColorManagementMode.DoNotColorManage);

                    using (var destinationStream = await destinationFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        BitmapEncoder encoder = await BitmapEncoder.CreateAsync(GetBitmapEncoder(destinationFile.FileType), destinationStream);
                        encoder.SetPixelData(decoder.BitmapPixelFormat, BitmapAlphaMode.Premultiplied, outTransformation.ScaledWidth, outTransformation.ScaledHeight, dpi, dpi, pixelData.DetachPixelData());
                        await encoder.FlushAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine("Resizing image failed with error:", e);
                return false;
            }

            return true;
        }

        private BitmapTransform GetTransformation(uint width, uint height, uint maxWidth, uint maxHeight)
        {
            var w = width;
            var h = height;
            var wScale = 1.0;
            var hScale = 1.0;
            if (w > maxWidth)
                wScale = (double)maxWidth / w;
            if (h > maxHeight)
                hScale = (double)maxHeight / h;
            var scale = Math.Min(wScale, hScale);

            return new BitmapTransform()
            {
                ScaledWidth = (uint)(w * scale),
                ScaledHeight = (uint)(h * scale)
            };
        }

        /// <summary>
        /// Gets the bitmap encoder of the given file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The corresponding bitmap encoder.</returns>
        private Guid GetBitmapEncoder(string fileExtension)
        {
            Guid encoderId;

            switch (fileExtension)
            {
                case "bmp":
                case ".bmp":
                    encoderId = BitmapEncoder.BmpEncoderId;
                    break;
                case "gif":
                case ".gif":
                    encoderId = BitmapEncoder.GifEncoderId;
                    break;
                case "png":
                case ".png":
                    encoderId = BitmapEncoder.PngEncoderId;
                    break;
                case "tif":
                case ".tif":
                    encoderId = BitmapEncoder.TiffEncoderId;
                    break;
                default:
                    encoderId = BitmapEncoder.JpegEncoderId;
                    break;
            }

            return encoderId;
        }
    }

}
