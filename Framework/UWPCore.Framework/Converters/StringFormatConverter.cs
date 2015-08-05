using System;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// The custom string format converter, which requires a parameter for formation {0:<parameter>}.
    /// </summary>
    public sealed class StringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts the object to a string with the given string format parameter.
        /// </summary>
        /// <param name="value">The datetime value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The short date time.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string paramString = parameter as string;
            if (paramString == null)
                return value.ToString();

            return string.Format("{0:" + paramString + "}", value);
        }

        /// <summary>
        /// Backwards conversion is not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
