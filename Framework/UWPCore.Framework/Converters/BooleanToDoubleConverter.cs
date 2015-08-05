using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that translates Boolean to Double values.
    /// Example: True to 0.1 and False to 0.9 when the parameter is "0.1;0.9".
    /// </summary>
    public sealed class BooleanToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean to a double given by the parameter value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter string in the form "x;y".</param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted double value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramString = parameter as string;

            if (paramString == null)
                return 0.0;

            var splitted = paramString.Split(';');

            if (splitted.Length == 1)
                return Double.Parse(splitted[0]);

            return (value is bool && (bool)value) ? double.Parse(splitted[0], CultureInfo.InvariantCulture) : double.Parse(splitted[1], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// This conversion method is not supported for this type.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

}
