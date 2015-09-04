using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.Threading.Tasks;

namespace UWPCore.Framework.Security.Cryptography
{
    [TestClass]
    public class DataProtectionProviderServiceTest
    {
        #region Fields

        /// <summary>
        /// Data protection provider service.
        /// </summary>
        DataProtectionProviderService dppSvc = null;

        #endregion

        #region Tests

        [TestInitialize]
        public void Initialize()
        {
            dppSvc = new DataProtectionProviderService();
        }

        [TestMethod]
        public async Task TestDataProtectionProviderForString()
        {
            const string input = "The quick brown fox jumps over the lazy dog.";
            const string expected = "The quick brown fox jumps over the lazy dog.";

            // Protect value
            IBuffer pBuffer = await dppSvc.ProtectAsync(input);

            // This is a weak comparison since we cannot predict what in the buffer is.
            // Also good to know we cannot use ConvertBinaryToString because it can happen that Unicode-chars 
            // dont exist.
            string actual = CryptographicBuffer.EncodeToBase64String(pBuffer);
            
            // Check if value is protected
            Assert.AreNotEqual(expected, actual, "Value was not protected.");
            
            // Unprotect data
            actual = await dppSvc.UnprotectAsync(pBuffer);
 
           // Check if value is unprotected successfully
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TestDataProtectionProviderForStringEmpty()
        {
            const string input = "";
            IBuffer expected = null;

            // Protect value
            IBuffer actual = await dppSvc.ProtectAsync(input);

            // Check if value is null
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TestDataProtectionProviderForStringFalse()
        {
            const string input = "The quick brown fox jumps over the lazy dog.";
            const string expected = null;

            // Protect value
            IBuffer pBuffer = await dppSvc.ProtectAsync(input);

            // Create other (raw) buffer
            var rBuffer = CryptographicBuffer.ConvertStringToBinary(input, dppSvc.Encoding);
            
            // Compare proteted buffer and raw buffer
            Assert.IsFalse(CryptographicBuffer.Compare(pBuffer, rBuffer));

            // Unprotect data
            string actual = await dppSvc.UnprotectAsync(rBuffer);

            // Check if value is unprotected successfully
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public async Task TestDataProtectionProviderForStream()
        {
            const string input = "The quick brown fox jumps over the lazy dog.";
            var inputStream = new InMemoryRandomAccessStream();

            var stream = inputStream.GetOutputStreamAt(0);
            using (var writer = new DataWriter(stream))
            {
                writer.WriteString(input);
                await writer.StoreAsync();
                await stream.FlushAsync();
            }

            // Protected data will placed in here.
            var protectedStream = new InMemoryRandomAccessStream();

            // Protect stream
            await dppSvc.ProtectStreamAsync(inputStream.GetInputStreamAt(0), 
                                            protectedStream.GetOutputStreamAt(0));

            {
                DataReader readerI = new DataReader(inputStream.GetInputStreamAt(0));
                DataReader readerP = new DataReader(protectedStream.GetInputStreamAt(0));
                await readerI.LoadAsync((uint)inputStream.Size);
                await readerP.LoadAsync((uint)protectedStream.Size);
                IBuffer bufferI = readerI.ReadBuffer((uint)inputStream.Size);
                IBuffer bufferP = readerP.ReadBuffer((uint)protectedStream.Size);

                // Verify that the protected data does not match the original
                Assert.IsFalse(CryptographicBuffer.Compare(bufferI, bufferP), "ProtectStreamAsync returned unprotected data");
            }

            // Unprotect stream
            var unprotectedStream = new InMemoryRandomAccessStream();

            // Unprotect stream
            await dppSvc.UnprotectStreamAsync(protectedStream.GetInputStreamAt(0),
                                              unprotectedStream.GetOutputStreamAt(0));

            {
                DataReader readerI = new DataReader(inputStream.GetInputStreamAt(0));
                DataReader readerP = new DataReader(unprotectedStream.GetInputStreamAt(0));
                await readerI.LoadAsync((uint)inputStream.Size);
                await readerP.LoadAsync((uint)unprotectedStream.Size);
                IBuffer bufferI = readerI.ReadBuffer((uint)inputStream.Size);
                IBuffer bufferP = readerP.ReadBuffer((uint)unprotectedStream.Size);

                // Verify that the unprotected data does match the original
                Assert.IsTrue(CryptographicBuffer.Compare(bufferI, bufferP));
            }
        }

        [TestMethod]
        public async Task TestDataProtectionProviderForStreamEmpty()
        {
            const string input = "";
            var inputStream = new InMemoryRandomAccessStream();

            var stream = inputStream.GetOutputStreamAt(0);
            using (var writer = new DataWriter(stream))
            {
                writer.WriteString(input);
                await writer.StoreAsync();
                await stream.FlushAsync();
            }

            // Protected data will placed in here.
            var protectedStream = new InMemoryRandomAccessStream();

            // Protect stream
            await dppSvc.ProtectStreamAsync(inputStream.GetInputStreamAt(0),
                                            protectedStream.GetOutputStreamAt(0));

            {
                DataReader readerI = new DataReader(inputStream.GetInputStreamAt(0));
                DataReader readerP = new DataReader(protectedStream.GetInputStreamAt(0));
                await readerI.LoadAsync((uint)inputStream.Size);
                await readerP.LoadAsync((uint)protectedStream.Size);
                IBuffer bufferI = readerI.ReadBuffer((uint)inputStream.Size);
                IBuffer bufferP = readerP.ReadBuffer((uint)protectedStream.Size);

                // Verify that the protected data does not match the original
                Assert.IsFalse(CryptographicBuffer.Compare(bufferI, bufferP), "ProtectStreamAsync returned unprotected data");
            }

            // Unprotect stream
            var unprotectedStream = new InMemoryRandomAccessStream();

            // Unprotect stream
            await dppSvc.UnprotectStreamAsync(protectedStream.GetInputStreamAt(0),
                                              unprotectedStream.GetOutputStreamAt(0));

            {
                DataReader readerI = new DataReader(inputStream.GetInputStreamAt(0));
                DataReader readerP = new DataReader(unprotectedStream.GetInputStreamAt(0));
                await readerI.LoadAsync((uint)inputStream.Size);
                await readerP.LoadAsync((uint)unprotectedStream.Size);
                IBuffer bufferI = readerI.ReadBuffer((uint)inputStream.Size);
                IBuffer bufferP = readerP.ReadBuffer((uint)unprotectedStream.Size);

                // Verify that the unprotected data does match the original
                Assert.IsTrue(CryptographicBuffer.Compare(bufferI, bufferP));
            }
        }

        [TestMethod]
        public async Task TestDataProtectionProviderForStreamOther()
        {
            const string input = "The quick brown fox jumps over the lazy dog.";
            var inputStream = new InMemoryRandomAccessStream();

            var stream = inputStream.GetOutputStreamAt(0);
            using (var writer = new DataWriter(stream))
            {
                writer.WriteString(input);
                await writer.StoreAsync();
                await stream.FlushAsync();
            }

            // Protected data will placed in here.
            var protectedStream = new InMemoryRandomAccessStream();

            // Protect stream
            await dppSvc.ProtectStreamAsync(inputStream.GetInputStreamAt(0),
                                            protectedStream.GetOutputStreamAt(0));

            {
                DataReader readerI = new DataReader(inputStream.GetInputStreamAt(0));
                DataReader readerP = new DataReader(protectedStream.GetInputStreamAt(0));
                await readerI.LoadAsync((uint)inputStream.Size);
                await readerP.LoadAsync((uint)protectedStream.Size);
                IBuffer bufferI = readerI.ReadBuffer((uint)inputStream.Size);
                IBuffer bufferP = readerP.ReadBuffer((uint)protectedStream.Size);

                // Verify that the protected data does not match the original
                Assert.IsFalse(CryptographicBuffer.Compare(bufferI, bufferP), "ProtectStreamAsync returned unprotected data");
            }

            // Unprotect stream
            var unprotectedStream = new InMemoryRandomAccessStream();

            await dppSvc.UnprotectStreamAsync(inputStream.GetInputStreamAt(0),
                                              unprotectedStream.GetOutputStreamAt(0));

            ulong expected = 0;
            ulong actual = unprotectedStream.Size;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
