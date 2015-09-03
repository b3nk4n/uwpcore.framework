using System;
using Windows.UI.Xaml.Data;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that converts enum values to its display value.
    /// </summary>
    public sealed class EnumDisplayValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts the enum value to the display value string.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The display value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return EnumExtensions.GetDisplayValue(value as Enum);
        }

        /// <summary>
        /// Converts the display value string to the enum value.
        /// </summary>
        /// <param name="value">The display value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The enum value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return EnumExtensions.GetByDisplayValue<Enum>(value as string);
        }
    }
}
