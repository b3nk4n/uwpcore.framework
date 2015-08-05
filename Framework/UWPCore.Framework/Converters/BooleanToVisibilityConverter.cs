using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that translates True to <see cref="Visibility.Visible"/> and False to
    /// <see cref="Visibility.Collapsed"/>. The conversion can be parameterized.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The value negation parameter.
        /// </summary>
        public const string PARAM_NEGATION = "!";

        /// <summary>
        /// Converts a boolean to a visiblity value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter. Use "!" to negate the result.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted visibility value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramString = parameter as string;

            if (paramString != null && paramString == PARAM_NEGATION)
                return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
            else
                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a visibility to a boolean value.
        /// </summary>
        /// <param name="value">The datetime value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter. Use "!" to negate the result.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The converted boolean value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var paramString = parameter as string;

            if (paramString != null && paramString == PARAM_NEGATION)
                return value is Visibility && (Visibility)value == Visibility.Collapsed;
            else
                return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }

}
