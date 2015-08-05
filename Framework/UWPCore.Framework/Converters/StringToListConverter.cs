using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Converter class to transform a comma seperated String to a list collection.
    /// </summary>
    public sealed class StringListConverter : IValueConverter
    {
        /// <summary>
        /// Converts a comma seperated string into a list of strings.
        /// </summary>
        /// <param name="value">The comma seperated string list</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The alternative seperator character. Default: ','.</param>
        /// <param name="language">The language, which is not used.</param>
        /// <returns>The list of seperated strings.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string))
                return new List<string>();

            char[] seperator = { ',' };
            string paramString = parameter as string;
            if (paramString != null && paramString.Length > 0)
            {
                seperator = paramString.ToCharArray();
            }

            return ((string)value).Split(seperator).ToList();
        }

        /// <summary>
        /// Backwards conversion is not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
