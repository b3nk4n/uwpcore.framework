using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Globalization.DateTimeFormatting;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPCore.Demo.Tasks
{
    //https://code.msdn.microsoft.com/windowsapps/Updating-a-tile-from-a-e0e492df/view/SourceCode#content
    public sealed class DiscoTileBackgroundTask : XamlRenderingBackgroundTask
    {
        // RenderTargetBitmap Pixel Data
        int pixelWidth;
        int pixelHeight;
        byte[] pixelData;

        // When the trigger for which this backgroudn task is fired, the OnRun method will be called. 
        // Since this background task is a XamlRenderingBackgroundTask, we can render XAML and use that 
        // image to update the app tile of this sample. 
        protected override async void OnRun(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            await GenerateHighResTileImageUpdateAsync("discoTile.xml", "discoTile.png", new Size(150, 150));

            UpdateTile("discoTile.png");

            deferral.Complete();
        }

        async Task GenerateHighResTileImageUpdateAsync(string inputMarkupFilename, string outputImageFilename, Size size)
        {
            var assetsFolder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var markupStorageFile = await assetsFolder.GetFileAsync(inputMarkupFilename);

            var stream = await markupStorageFile.OpenReadAsync();
            string markupContent;
            using (var sr = new StreamReader(stream.AsStreamForRead()))
            {
                markupContent = await sr.ReadToEndAsync();
            }

            // Parse our custom tile XAML and create the object tree for this XAML
            Border border = (Border)XamlReader.Load(markupContent);

            // Create a new semi-transparent backgroud brush for the border, with a randomly chosen color.
            // This sample is setting a color for illustration purposes. 
            // Transparency is also supported in tiles. 
            // To make the tile transparent, so that the start screen background picture that your user selected shows through the
            // tile, set Background color property in the Package.appxmanifest to "transparent".
            border.Background = GetRandomBackgroundBrush();

            // Change the text of the Timestamp TextBlock.
            Grid grid = (Grid)border.Child;
            TextBlock titleText = (TextBlock)grid.FindName("Title");
            titleText.Text = string.Format("{0:hh:mm}", DateTime.Now);

            // Set the source for the image that is displayed on the tile. 
            Image logoImage = (Image)grid.FindName("LogoImage");
            var bitmapImage = new BitmapImage() { CreateOptions = BitmapCreateOptions.None };
            bitmapImage.UriSource = new Uri("ms-appx:///Assets/disco-ball.png");
            logoImage.Source = bitmapImage;
            
            // Render the XAML, in this example a Border and its content, to a bitmap and save the bitmap to a file. 
            await RenderAndSaveToFileAsync(border, outputImageFilename, (int)size.Width, (int)size.Height);
        }

        async Task RenderAndSaveToFileAsync(UIElement uiElement, string outputImageFilename, int width, int height)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(uiElement, width, height);
            pixelWidth = rtb.PixelWidth;
            pixelHeight = rtb.PixelHeight;
            var buffer = await rtb.GetPixelsAsync();

            StorePixelsFromBuffer(buffer);
            await WriteBufferToFile(outputImageFilename);
        }

        void StorePixelsFromBuffer(IBuffer buffer)
        {
            DataReader dataReader = DataReader.FromBuffer(buffer);
            var data = new byte[buffer.Length];
            dataReader.ReadBytes(data);

            pixelData = data;
        }

        async Task WriteBufferToFile(string outputImageFilename)
        {
            var outputStorageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(outputImageFilename, Windows.Storage.CreationCollisionOption.ReplaceExisting);            
            var outputStream = await outputStorageFile.OpenAsync(FileAccessMode.ReadWrite);
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, outputStream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)pixelWidth, (uint)pixelHeight, 96, 96, pixelData);
            await encoder.FlushAsync();

            pixelData = null;
        }

        // Send a tile notification with the new tile payload. 
        void UpdateTile(string tileUpdateImagePath)
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdater.Clear();
            var tileTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            var tileImageAttributes = tileTemplate.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appdata:///local/discoTile.png");

            //XmlElement tmp = tileImageAttributes.Item(0) as XmlElement;
            //tmp.SetAttribute("src", "UpdatedLiveTile.png");


            ////((XmlElement)tileImageAttributes.Item(0)).SetAttribute("src", "ms-appdata:///local/updatedTile.png");
            var notification = new TileNotification(tileTemplate);
            tileUpdater.Update(notification);

            //XmlDocument mediumTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            //XmlNodeList mediumTileImageAttributes = mediumTileXml.GetElementsByTagName("image");
            //((XmlElement)mediumTileImageAttributes[0]).SetAttribute("src", "ms-appdata:///local/" + fileNameMedium);
            //((XmlElement)mediumTileImageAttributes[0]).SetAttribute("alt", "AltString");

        }

        // Helper method to get the current time to be used when updating the tile in this sample.
        string GetCurrentTime()
        {
            DateTime datetime = DateTime.Now;
            DateTimeFormatter datetimeFormat = DateTimeFormatter.ShortTime;
            return datetimeFormat.Format(datetime);
        }


        // Helper method to get a random background color to be used when updating the tile in this sample.
        Brush GetRandomBackgroundBrush()
        {
            // Seed our random number generator.
            Random rand = new Random();
            return new SolidColorBrush(Windows.UI.ColorHelper.FromArgb(255, 0, (byte)(rand.Next() % 255), (byte)(rand.Next() % 255)));
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            System.Diagnostics.Debug.WriteLine("Run called!");
            OnRun(taskInstance);
        }
    }
}
