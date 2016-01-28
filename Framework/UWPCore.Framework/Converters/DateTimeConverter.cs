using System;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Converter class to convert date time objects.
    /// </summary>
    /// <seealso cref="http://samples.pdmlab.com/stringformatting"/>
    public class DateTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DateTime(Offset) object to a formated date and time string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>The formated string.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null && value is DateTime || value is DateTimeOffset)
                return string.Format("{0:g}", value); // example: 15.11.2015 23:18

            return value;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
