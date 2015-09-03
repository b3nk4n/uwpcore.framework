using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UWPCore.Framework.Storage;

namespace UWPCore.Test.Storage
{
    [TestClass]
    public class RoamingObjectTest
    {
        /// <summary>
        /// The used test key.
        /// </summary>
        private const string TEST_KEY = "test_roaming_key";

        /// <summary>
        /// The used default value.
        /// </summary>
        private const string DEFAULT_VALUE = "default_value";

        /// <summary>
        /// The system under test.
        /// </summary>
        private StoredObjectBase<string> _storedObject;

        [TestInitialize]
        public void Initialize()
        {
            _storedObject = new RoamingObject<string>(TEST_KEY, DEFAULT_VALUE);
        }

        [TestMethod]
        public void TestDefaultValue()
        {
            Assert.AreEqual(DEFAULT_VALUE, _storedObject.DefaultValue);
            Assert.AreEqual(DEFAULT_VALUE, _storedObject.Value);
        }

        [TestMethod]
        public void TestWriteAndReadValue()
        {
            string data = "data";

            // change data
            _storedObject.Value = data;

            Assert.AreEqual(data, _storedObject.Value);
        }

        [TestMethod]
        public void TestReadKey()
        {
            Assert.AreEqual(TEST_KEY, _storedObject.Key);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _storedObject.Delete();
        }
    }

}
