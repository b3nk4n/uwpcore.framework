using System;
using UWPCore.Framework.Mvvm;
using Windows.ApplicationModel.Contacts;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPCore.Demo.ViewModels
{
    /// <summary>
    /// View model for contact. 
    /// This wrapper is necessary because the Images come as IRandomAccessStreamReference. Alternatively use a converter.
    /// </summary>
    public sealed class ContactViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Model
        /// </summary>
        Contact model;

        private string firstName;
        private string lastName;
        private ImageSource thumbnail;

        #endregion

        #region Properties
        
        /// <summary>
        /// Get or set first name.
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            private set { Set(ref firstName, value); }
        }
        
        /// <summary>
        /// Get or set last name.
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            private set { Set(ref lastName, value); }
        }

        /// <summary>
        /// Get or set thumbnail.
        /// </summary>
        public ImageSource Thumbnail
        {
            get { return thumbnail; }
            private set { Set(ref thumbnail, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize object.
        /// </summary>
        /// <param name="model"></param>
        public ContactViewModel(Contact model)
        {
            this.model = model;

            lastName = model.LastName;
            firstName = model.FirstName;
            if(model.Thumbnail != null)
                CreateThumbnail();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create thumbnail bitmap image from stream.
        /// </summary>
        public async void CreateThumbnail()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.DecodePixelHeight = 100;
            bitmapImage.DecodePixelWidth = 100;
            using (IRandomAccessStream fs = await model.Thumbnail.OpenReadAsync())
            {
                await bitmapImage.SetSourceAsync(fs);
            }

            Thumbnail = bitmapImage;
        }

        #endregion
    }
}
