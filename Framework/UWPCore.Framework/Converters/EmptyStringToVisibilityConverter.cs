using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that translates any non empty string to <see cref="Visibility.Visible"/> 
    /// and a whitespace string to <see cref="Visibility.Collapsed"/>.
    /// The conversion can be parameterized and negated.
    /// </summary>
    public sealed class EmptyStringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The value negation parameter.
        /// </summary>
        public const string PARAM_NEGATION = "!";

        /// <summary>
        /// Converts a string value to a visiblity value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter. Use "!" to negate the result.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted visibility value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramString = parameter as string;
            var stringValue = value as string;

            if (paramString != null && paramString == PARAM_NEGATION)
                return (string.IsNullOrWhiteSpace(stringValue)) ? Visibility.Visible : Visibility.Collapsed;
            else
                return (string.IsNullOrWhiteSpace(stringValue)) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

}
