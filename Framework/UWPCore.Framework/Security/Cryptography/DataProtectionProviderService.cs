using System;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Security.Cryptography
{
    /// <summary>
    /// The data protection service use the data protection provider to protect data. 
    /// https://msdn.microsoft.com/en-us/library/windows/apps/windows.security.cryptography.dataprotection.dataprotectionprovider.aspx
    /// 
    /// On default the data will be protected to LOCAL=user. That means nobody else than the LOCAL user can unprotect the data.
    /// </summary>
    public sealed class DataProtectionProviderService  : IDataProtectionProviderService
    {
        #region Fields

        /// <summary>
        /// Used encoding of data protection provider service.
        /// </summary>
        private BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;

        #endregion

        #region Properties

        /// <summary>
        /// Get the used encoding.
        /// </summary>
        public BinaryStringEncoding Encoding
        {
            get { return encoding; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Protect the value with data protection provider.
        /// </summary>
        /// <param name="value">Value that needs to be protected.</param>
        /// <param name="protectionDescriptor">The entity to which the data will be protected. Default is set to LOCAL=user.</param>
        /// <returns>Returns the protected data as buffer of binary data.</returns>
        public async Task<IBuffer> ProtectAsync(string value, string protectionDescriptor = "LOCAL=user")
        {
            // Encode value
            IBuffer encodedValue = CryptographicBuffer.ConvertStringToBinary(value, encoding);

            if (encodedValue == null)
                return null;

            var provider = new DataProtectionProvider(protectionDescriptor);

            // Protect data
            IBuffer protectedData = await provider.ProtectAsync(encodedValue);

            return protectedData;
        }

        /// <summary>
        /// Unprotect data with data protection provider.
        /// </summary>
        /// <param name="data">Data to unprotect.</param>
        /// <returns>Returns the value as string.</returns>
        public async Task<string> UnprotectAsync(IBuffer data)
        {
            string value = null;
            
            var provider = new DataProtectionProvider();    // No provider needs to be set.

            try
            {
                // Unprotect data
                IBuffer unprotectedData = await provider.UnprotectAsync(data);

                // Encode value
                value = CryptographicBuffer.ConvertBinaryToString(encoding, unprotectedData);
            }
            catch(Exception)
            {

            }

            return value;
        }

        /// <summary>
        /// Protect stream data with data protection provider.
        /// </summary>
        /// <param name="source">The source stream contains the unprotected data.</param>
        /// <param name="destination">The destination stream contains the protected data.</param>
        /// <param name="protectionDescriptor">The entity to which the data will be protected. Default is set to LOCAL=user.</param>
        /// <returns></returns>
        public async Task ProtectStreamAsync(IInputStream source, IOutputStream destination, string protectionDescriptor = "LOCAL=user")
        {
            var provider = new DataProtectionProvider(protectionDescriptor);

            await provider.ProtectStreamAsync(source, destination);
            await destination.FlushAsync();
        }

        /// <summary>
        /// Unprotect stream data with data protection provider.
        /// </summary>
        /// <param name="source">The source stream contains the protected data.</param>
        /// <param name="destination">The destination stream contains the unprotected data.</param>
        /// <returns></returns>
        public async Task UnprotectStreamAsync(IInputStream source, IOutputStream destination)
        {
            var provider = new DataProtectionProvider();

            try
            {
                await provider.UnprotectStreamAsync(source, destination);
                await destination.FlushAsync();
            }
            catch(Exception)
            {
                
            }
        }

        #endregion
    }
}
