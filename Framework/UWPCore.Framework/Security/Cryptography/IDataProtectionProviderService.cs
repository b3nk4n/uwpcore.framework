using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Security.Cryptography
{
    public interface IDataProtectionProviderService
    {
        /// <summary>
        /// Get the used encoding.
        /// </summary>
        BinaryStringEncoding Encoding { get; }

        /// <summary>
        /// Protect the value with data protection provider.
        /// </summary>
        /// <param name="value">Value that needs to be protected.</param>
        /// <param name="protectionDescriptor">The entity to which the data will be protected. Default is set to LOCAL=user.</param>
        /// <returns>Returns the protected data as buffer of binary data.</returns>
        Task<IBuffer> ProtectAsync(string value, string protectionDescriptor = "LOCAL=user");

        /// <summary>
        /// Unprotect data with data protection provider.
        /// </summary>
        /// <param name="data">Data to unprotect.</param>
        /// <returns>Returns the value as string.</returns>
        Task<string> UnprotectAsync(IBuffer data);

        /// <summary>
        /// Protect stream data with data protection provider.
        /// </summary>
        /// <param name="source">The source stream contains the unprotected data.</param>
        /// <param name="destination">The destination stream contains the protected data.</param>
        /// <param name="protectionDescriptor">The entity to which the data will be protected. Default is set to LOCAL=user.</param>
        /// <returns></returns>
        Task ProtectStreamAsync(IInputStream source, IOutputStream destination, string protectionDescriptor = "LOCAL=user");

        /// <summary>
        /// Unprotect stream data with data protection provider.
        /// </summary>
        /// <param name="source">The source stream contains the protected data.</param>
        /// <param name="destination">The destination stream contains the unprotected data.</param>
        /// <returns></returns>
        Task UnprotectStreamAsync(IInputStream source, IOutputStream destination);
    }
}
