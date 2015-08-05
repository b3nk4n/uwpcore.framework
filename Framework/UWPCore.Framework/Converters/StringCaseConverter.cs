using System;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Converter class to convert upper case or lower case Strings.
    /// </summary>
    public sealed class StringCaseConverter : IValueConverter
    {
        /// <summary>
        /// The lower case parameter.
        /// </summary>
        public const string PARAM_LOWER_CASE = "lower";

        /// <summary>
        /// The upper case parameter.
        /// </summary>
        public const string PARAM_UPPER_CASE = "upper";

        /// <summary>
        /// Converts a String to upper case (default) or lower case.
        /// </summary>
        /// <param name="value">The String to convert.</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The parameter, which is 'upper' (default) or 'lower'.</param>
        /// <param name="language">The language, which is not used.</param>
        /// <returns>The case converted String.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                string paramString = parameter as string;
                if (paramString == null || paramString == PARAM_LOWER_CASE)
                    return stringValue.ToLower();
                else
                    return stringValue.ToUpper();
            }

            else
                return value;
        }

        /// <summary>
        /// Backward converstion is not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
