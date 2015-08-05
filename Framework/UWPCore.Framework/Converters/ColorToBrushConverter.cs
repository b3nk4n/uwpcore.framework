using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Converts a <see cref="Windows.UI.Color"/> to a <see cref="SolidColorBrush"/> and vice versa.
    /// </summary>
    public sealed class ColorToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="Windows.UI.Color"/> to a <see cref="SolidColorBrush"/> instance.
        /// </summary>
        /// <param name="value">The color value to convert.</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The parameter, which is not used.</param>
        /// <param name="language">The language, which is not used.</param>
        /// <returns>The converted brush.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Color)
            {
                var color = (Color)value;
                return new SolidColorBrush(color);
            }
            return null;
        }

        /// <summary>
        /// Converts a <see cref="SolidColorBrush"/> to a <see cref="Windows.UI.Color"/> instance.
        /// </summary>
        /// <param name="value">The brush value to convert.</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The parameter, which is not used.</param>
        /// <param name="language">The language, which is not used.</param>
        /// <returns>The converted color.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var solidColor = value as SolidColorBrush;
            if (solidColor != null)
            {
                return solidColor.Color;
            }
            return Colors.Transparent;
        }
    }

}
