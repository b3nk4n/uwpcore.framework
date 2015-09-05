using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace UWPCore.Framework.Security.Cryptography
{
    [TestClass]
    public class Aes256ServiceTest
    {
        /// <summary>
        /// Aes256 service to encrypt and decrypt data.
        /// </summary>
        ISymmetricAlgorithmService aes256Service;

        [TestInitialize]
        public void Initialize()
        {
            aes256Service = new Aes256Service();
        }

        [TestMethod]
        public void TestAes256Encrypt()
        {
            const string input = "The quick brown fox jumps over the lazy dog";
            const string key = @"abcd1234!";

            IBuffer expected = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
            IBuffer actual = aes256Service.Encrypt(input, key);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void TestAes256Decrypt()
        {
            const string input = "The quick brown fox jumps over the lazy dog";
            const string expected = "The quick brown fox jumps over the lazy dog";
            const string key = @"abcd1234!";

            IBuffer encrypted = aes256Service.Encrypt(input, key);

            Assert.IsNotNull(encrypted);

            string actual = aes256Service.Decrypt(encrypted, key);
            Assert.AreEqual(expected, actual);
        }
    }
}
