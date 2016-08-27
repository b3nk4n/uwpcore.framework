using System;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that translates <see cref="bool"/> to <see cref="bool?"/>.
    /// </summary>
    public sealed class BooleanToNullableConverter : IValueConverter
    {
        /// <summary>
        /// Converts the value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The nullable value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool? nullable = (bool)value;
            return nullable;
        }

        /// <summary>
        /// Converts the nullable boolean value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The bool value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool? nullable = (bool?)value;
            if (nullable.HasValue)
                return nullable.Value;
            else
                return false;
        }
    }
}
