using System;
using UWPCore.Framework.UI;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts the given boolean value to a bush color specified by the parameters.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">
        /// The color parameters in format "#xxxxxx;#xxxxxx", where the first value is the True value
        /// and the seconds is the False value.
        /// </param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted color brush value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramString = (string)parameter;
            var boolValue = (bool)value;

            if (string.IsNullOrEmpty(paramString))
                throw new ArgumentException("BooleanToBrushConverter requires a parameter.");

            var splittedColors = paramString.Split(';');

            if (splittedColors.Length != 2)
                throw new ArgumentException("BooleanToBrushConverter requires two parameters.");

            Color colorTrue = ColorConverter.FromHex(splittedColors[0]);
            Color colorFalse = ColorConverter.FromHex(splittedColors[1]);

            return (boolValue) ? new SolidColorBrush(colorTrue) : new SolidColorBrush(colorFalse);
        }

        /// <summary>
        /// Not implemented, because no use case for this is known up to now. But in fact could be implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
