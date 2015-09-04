using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UWPCore.Framework.Storage;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using UWPCore.Framework.Graphics.Controls;

namespace UWPCore.Framework.Graphics
{
    [TestClass]
    public class GraphicsServiceTest
    {
        private const string TEST_FILE_PATH = "GraphicsTest/sampleControl300.png";

        private IGraphicsService _graphicsService;

        private IStorageService _storageService;

        [TestInitialize]
        public void Initialize()
        {
            _graphicsService = new GraphicsService();
            _storageService = new LocalStorageService();
        }

        [TestMethod]
        [Ignore]
        public async Task TestRenderUIElementAsync()
        {
            var taskSource = new TaskCompletionSource<object>();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var file = await _storageService.CreateOrGetFileAsync(TEST_FILE_PATH);
                        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            var uiElement = new SampleControl300();
                            RenderTargetBitmap res = await _graphicsService.RenderToStreamAsync(uiElement, stream, BitmapEncoder.PngEncoderId);

                            Assert.IsNotNull(res); // fails, because the UI element has to be added to the visual tree before
                            Assert.AreEqual(300, res.PixelHeight);
                            Assert.AreEqual(300, res.PixelWidth);
                        }

                        taskSource.SetResult(null);
                    }
                    catch (Exception e)
                    {
                        taskSource.SetException(e);
                    }
                });
            await taskSource.Task;
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _storageService.DeleteFileAsync(TEST_FILE_PATH);
        }
    }
}