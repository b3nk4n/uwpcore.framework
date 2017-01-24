using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Security.Cryptography
{
    /// <summary>
    /// This service use Aes 256 with CBC and Pkcs7 to encrypt and decrypt data. The password is simple hashed with Sha256.
    /// The IV is generated randomly for each encryption and append before the encrypted data. In this service the IV has 
    /// a length of 16 Byte.
    /// </summary>
    public sealed class Aes256Service : ISymmetricAlgorithmService
    {
        /// <summary>
        /// Encrypt value using password.
        /// </summary>
        /// <param name="value">Value to encrypt.</param>
        /// <param name="key">Key that is used for encryption.</param>
        /// <returns>A buffer with the encrypted data is returned.</returns>
        public IBuffer Encrypt(string value, string key)
        {
            // Create a Sha256 from key.
            var passwordBuffer = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);
            var hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer keyMaterial = hashProvider.HashData(passwordBuffer);

            // Create an Aes256 with CBC and Pkcs7
            var aesProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            CryptographicKey aesKey = aesProvider.CreateSymmetricKey(keyMaterial);

            // Create a random IV so the password can be used more than once.
            IBuffer iv = CryptographicBuffer.GenerateRandom(aesProvider.BlockLength);

            // Encrypt value.
            var data = CryptographicBuffer.ConvertStringToBinary(value, BinaryStringEncoding.Utf8);
            IBuffer encrypted = CryptographicEngine.Encrypt(aesKey, data, iv);

            // Insert random generated IV before encrypted message because it will be needed at decryption.
            IBuffer result = CryptographicBuffer.CreateFromByteArray(new byte[iv.Length + encrypted.Length]);
            iv.CopyTo(0, result, 0, iv.Length);
            encrypted.CopyTo(0, result, iv.Length, encrypted.Length);

            return result;
        }

        /// <summary>
        /// Decrypt buffer using password.
        /// </summary>
        /// <param name="data">Data buffer to decrypt.</param>
        /// <param name="key">Key that is used for decryption.</param>
        /// <returns>Returns the decrypted value as string.</returns>
        public string Decrypt(IBuffer data, string key)
        {
            // Create a Sha256 from key.
            var passwordBuffer = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);
            var hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer keyMaterial = hashProvider.HashData(passwordBuffer);

            // Create am Aes256 with CBC and Pkcs7.
            var aesProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            CryptographicKey aesKey = aesProvider.CreateSymmetricKey(keyMaterial);

            // Split data into IV and encrypted data.
            IBuffer iv = CryptographicBuffer.CreateFromByteArray(new byte[aesProvider.BlockLength]);
            IBuffer encrypted = CryptographicBuffer.CreateFromByteArray(new byte[data.Length - iv.Length]);

            data.CopyTo(0, iv, 0, iv.Length);
            data.CopyTo(iv.Length, encrypted, 0, encrypted.Length);

            // Decrypt data.
            IBuffer decrypted = CryptographicEngine.Decrypt(aesKey, encrypted, iv);
            string value = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, decrypted);

            return value;
        }
    }
}
