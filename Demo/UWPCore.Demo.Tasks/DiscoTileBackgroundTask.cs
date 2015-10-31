using System;
using System.IO;
using System.Threading.Tasks;
using UWPCore.Framework.Storage;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPCore.Demo.Tasks
{
    // https://code.msdn.microsoft.com/windowsapps/Updating-a-tile-from-a-e0e492df/view/SourceCode#content
    // TODO: Crash if using IBackgroundTask because of different task contexts.
    // TODO: Sometimes Tile disappears
    public sealed class DiscoTileBackgroundTask : XamlRenderingBackgroundTask //, IBackgroundTask
    {
        #region Fields

        ILocalStorageService localStorageService;
        Random random;

        #endregion

        #region Constructors

        public DiscoTileBackgroundTask()
        {
            localStorageService = new LocalStorageService();
            random = new Random();
        }

        #endregion

        #region Methods

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            System.Diagnostics.Debug.WriteLine("Run called!");
            OnRun(taskInstance);
        }

        // When the trigger for which this backgroudn task is fired, the OnRun method will be called. 
        // Since this background task is a XamlRenderingBackgroundTask, we can render XAML and use that 
        // image to update the app tile of this sample. 
        protected override async void OnRun(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            await CreateAndUpdateDiscoTileAsync("discoTile.xml", "Tiles/discoTile.png", new Size(150, 150));

            deferral.Complete();
        }

        /// <summary>
        /// Creates the DiscoTile image and update the tile.
        /// </summary>
        /// <param name="inputMarkupFilename"></param>
        /// <param name="outputImageFilename"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private async Task CreateAndUpdateDiscoTileAsync(string inputMarkupFilename, string outputImageFilename, Size size)
        {
            // Get Tile markup
            var assetsFolder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var markupStorageFile = await assetsFolder.GetFileAsync(inputMarkupFilename);

            // Update Elements
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
            border.Background = new SolidColorBrush(Windows.UI.ColorHelper.FromArgb(255, 0, (byte)(random.Next() % 255), (byte)(random.Next() % 255)));

            // Change the text of the Title TextBlock.
            Grid grid = (Grid)border.Child;
            TextBlock titleText = (TextBlock)grid.FindName("Title");
            titleText.Text = string.Format("{0:hh:mm}", DateTime.Now);

            // Set the source for the image that is displayed on the tile. 
            Image logoImage = (Image)grid.FindName("LogoImage");
            var bitmapImage = new BitmapImage() { CreateOptions = BitmapCreateOptions.None };
            bitmapImage.UriSource = new Uri("ms-appx:///Assets/disco-ball.png");
            logoImage.Source = bitmapImage;

            // Render DiscoTile
            RenderTargetBitmap rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(border, (int)size.Width, (int)size.Height);
            var buffer = await rtb.GetPixelsAsync();

            // Read buffer into byte array.
            DataReader dataReader = DataReader.FromBuffer(buffer);
            var data = new byte[buffer.Length];
            dataReader.ReadBytes(data);

            // Save DiscoTile to local storage
            var outputStorageFile = await localStorageService.CreateOrGetFileAsync(outputImageFilename);
            var outputStream = await outputStorageFile.OpenAsync(FileAccessMode.ReadWrite);
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, outputStream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)size.Width, (uint)size.Height, 96, 96, data);
            await encoder.FlushAsync();

            // Update Tile with image.
            UpdateTile(outputImageFilename);
        }

        // Send a tile notification with the new tile payload. 
        void UpdateTile(string tileUpdateImagePath)
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdater.Clear();
            var tileTemplate = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            var tileImageAttributes = tileTemplate.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", string.Format("ms-appdata:///local/{0}", tileUpdateImagePath));

            var notification = new TileNotification(tileTemplate);
            tileUpdater.Update(notification);
        }

        #endregion
    }
}
