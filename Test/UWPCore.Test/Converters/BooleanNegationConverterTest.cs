using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPCore.Framework.Converters;
using Windows.UI.Xaml.Data;

namespace UWPCore.Test.Converters
{
    [TestClass]
    public class BooleanNegationConverterTest
    {
        private IValueConverter _converter;

        [TestInitialize]
        public void Initialize()
        {
            _converter = new BooleanNegationConverter();
        }

        public void TestConvertTrueValue()
        {
            bool value = (bool)_converter.Convert(true, null, null, null);
            Assert.IsFalse(value);
        }

        public void TestConvertFalseValue()
        {
            bool value = (bool)_converter.Convert(false, null, null, null);
            Assert.IsTrue(value);
        }

        public void TestConvertBackTrueValue()
        {
            bool value = (bool)_converter.Convert(true, null, null, null);
            Assert.IsFalse(value);
        }

        public void TestConvertBackFalseValue()
        {
            bool value = (bool)_converter.Convert(false, null, null, null);
            Assert.IsTrue(value);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _converter = null;
        }
    }
}
