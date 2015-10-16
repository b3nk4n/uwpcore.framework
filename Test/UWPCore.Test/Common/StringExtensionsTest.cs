using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UWPCore.Framework.Common
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void TestFirstLetterToLower()
        {
            var value = "HelloWorld";
            Assert.AreEqual("helloWorld", value.FirstLetterToLower());
        }

        [TestMethod]
        public void TestAllIndexesOfEmtpyString()
        {
            var value = "";
            var res = value.AllIndexesOf(new[] { ',', '.' });
            Assert.AreEqual(0, res.Length);
        }

        [TestMethod]
        public void TestAllIndexesOfStringWithoutSeparators()
        {
            var value = "Hello world";
            var res = value.AllIndexesOf(new[] { ',', '.' });
            Assert.AreEqual(0, res.Length);
        }

        [TestMethod]
        public void TestAllIndexesOfStringWithSeparators()
        {
            var value = "Hello world, this is a beautiful day.";
            var res = value.AllIndexesOf(new[] { ',', '.' });
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(11, res[0]);
            Assert.AreEqual(36, res[1]);
        }

        [TestCleanup]
        public void Cleanup()
        { 
        }
    }
}
