using Windows.Storage.Streams;

namespace UWPCore.Framework.Security.Cryptography
{
    /// <summary>
    /// Interface for symmetric algorithms that are used to encrypt and decrypt data such as
    /// AES, DES, ...
    /// </summary>
    public interface ISymmetricAlgorithmService
    {
        /// <summary>
        /// Interface method to encrypt value with key.
        /// </summary>
        /// <param name="value">Value to encrypt.</param>
        /// <param name="key">Key that is used for encryption.</param>
        /// <returns>Return buffer with encrypted data.</returns>
        IBuffer Encrypt(string value, string key);

        /// <summary>
        /// Interface method to decrypt buffer with key.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <param name="key">Key that is used for decryption.</param>
        /// <returns></returns>
        string Decrypt(IBuffer data, string key);
    }
}
