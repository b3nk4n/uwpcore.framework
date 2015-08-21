namespace UWPCore.Framework.Common
{
    /// <summary>
    /// Extensin methods for <see cref="string"/>.
    /// </summary>
    public  static class StringExtensions
    {
        /// <summary>
        /// Transforms the first letter to lower case.
        /// </summary>
        /// <param name="str">The string to manipulate.</param>
        /// <returns>The transformed string.</returns>
        public static string FirstLetterToLower(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToLower(str[0]) + str.Substring(1);

            return str.ToLower();
        }
    }
}
