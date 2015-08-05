using System;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that translates Boolean values to String values.
    /// Example: True to "On" and False to "Off" for the parameter "On;Off".
    /// </summary>
    public sealed class BooleanToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean to a string given by the parameter value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter string in the form "x,y".</param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted string value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramString = parameter as string;

            if (paramString == null)
                return string.Empty;

            var splitted = paramString.Split(';');

            if (splitted.Length == 1)
                return splitted[0];

            return (value is bool && (bool)value) ? splitted[0] : splitted[1];
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
