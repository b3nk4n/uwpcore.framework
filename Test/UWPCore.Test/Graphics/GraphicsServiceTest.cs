using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UWPCore.Framework.Graphics;
using UWPCore.Framework.Storage;
using UWPCore.Test.Graphics.Controls;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

namespace UWPCore.Test.Graphics
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
        public async Task TestRenderUIElement()
        {
            var file = await _storageService.CreateOrGetFileAsync(TEST_FILE_PATH);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            
            RenderTargetBitmap res = await _graphicsService.RenderToStreamAsync(new SampleControl300(), stream, BitmapEncoder.PngEncoderId);

            var pixels = await res.GetPixelsAsync();

            Assert.IsNotNull(res);
            Assert.AreEqual(300, res.PixelHeight);
            Assert.AreEqual(300, res.PixelWidth);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _storageService.DeleteFileAsync(TEST_FILE_PATH);
        }
    }
}