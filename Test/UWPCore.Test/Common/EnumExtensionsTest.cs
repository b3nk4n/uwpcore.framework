using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UWPCore.Framework.Common
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void TestDisplayValue()
        {
            var value = SampleEnum.LowValue;
            Assert.AreEqual("Low value", value.GetDisplayValue());
        }

        [TestMethod]
        public void TestDefaultValue()
        {
            var defaultValue = EnumExtensions.GetDefaultValue<SampleEnum>();
            Assert.AreEqual(SampleEnum.MediumValue, defaultValue);
        }

        [TestMethod]
        public void TestGetEnumValuesAsList()
        {
            var values = EnumExtensions.GetAsList<SampleEnum>();
            Assert.AreEqual(3, values.Count);
        }

        [TestMethod]
        public void TestGetEnumDispalyValuesAsList()
        {
            var values = EnumExtensions.GetDisplayValueList<SampleEnum>();
            Assert.AreEqual("Low value", values[0]);
            Assert.AreEqual("Medium value", values[1]);
            Assert.AreEqual("High value", values[2]);
        }

        [TestMethod]
        public void TestGetEnumByDisplayValue()
        {
            var value = EnumExtensions.GetByDisplayValue<SampleEnum>("High value");
            Assert.AreEqual(SampleEnum.HighValue, value);
        }

        [TestCleanup]
        public void Cleanup()
        { 
        }
    }

    internal enum SampleEnum
    {
        [Display(Name = "Low value")]
        LowValue,
        [DefaultValue(true)]
        [Display(Name = "Medium value")]
        MediumValue,
        [Display(Name = "High value")]
        HighValue
    }
}
