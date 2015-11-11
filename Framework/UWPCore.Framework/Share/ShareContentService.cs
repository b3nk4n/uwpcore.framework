using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Share
{
    /// <summary>
    /// Service class to share content.
    /// </summary>
    public class ShareContentService : IShareContentService
    {
        /// <summary>
        /// The share data to store it temporary.
        /// </summary>
        private ShareData _shareData = new ShareData();

        public void ShareWebLink(string title, Uri link, string description = "")
        {
            Register(ShareWebLinkHandler);

            _shareData.Title = title;
            _shareData.Description = description;
            _shareData.Link = link;

            DataTransferManager.ShowShareUI();
        }

        private void ShareWebLinkHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            Unregister(ShareWebLinkHandler);

            DataRequest request = e.Request;
            request.Data.Properties.Title = _shareData.Title;
            request.Data.Properties.Description = _shareData.Description;
            request.Data.SetWebLink(_shareData.Link);
        }

        public void ShareText(string title, string text, string description = "")
        {
            Register(ShareTextHandler);

            _shareData.Title = title;
            _shareData.Description = description;
            _shareData.Text = text;

            DataTransferManager.ShowShareUI();
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            Unregister(ShareTextHandler);

            DataRequest request = e.Request;
            request.Data.Properties.Title = _shareData.Title;
            request.Data.Properties.Description = _shareData.Description;
            request.Data.SetText(_shareData.Text);
        }

        public void ShareImage(string title, IStorageFile imageFile, IStorageFile thumbnailFile = null, string text = null, string description = "")
        {
            Register(ShareImageHandler);

            _shareData.Title = title;
            _shareData.Text = text;
            _shareData.Description = description;
            _shareData.Thumbnail = thumbnailFile;
            _shareData.Image = imageFile;
            DataTransferManager.ShowShareUI();
        }

        private void ShareImageHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            Unregister(ShareImageHandler);

            DataRequest request = e.Request;
            request.Data.Properties.Title = _shareData.Title;
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(_shareData.Image));
            if (_shareData.Thumbnail != null)
                request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(_shareData.Thumbnail);
            if (_shareData.Text != null)
                request.Data.SetText(_shareData.Text);
            request.Data.Properties.Description = _shareData.Description;
        }

        /// <summary>
        /// Registers an share content handler.
        /// </summary>
        /// <param name="handler">The share content handler.</param>
        private void Register(TypedEventHandler<DataTransferManager,DataRequestedEventArgs> handler)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += handler;
        }

        /// <summary>
        /// Unregisters an share content handler.
        /// </summary>
        /// <param name="handler">The share content handler.</param>
        private void Unregister(TypedEventHandler<DataTransferManager, DataRequestedEventArgs> handler)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= handler;
        }

        /// <summary>
        /// Simple class to store share data.
        /// </summary>
        private class ShareData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public Uri Link { get; set; }
            public string Text { get; set; }
            public IStorageFile Thumbnail { get; set; }
            public IStorageFile Image { get; set; }
        }
    }
}
