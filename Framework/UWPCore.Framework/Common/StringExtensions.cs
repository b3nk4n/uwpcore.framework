namespace UWPCore.Framework.Common
{
    /// <summary>
    /// Extensin methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
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

        /// <summary>
        /// Gets all indexes of the string that are equal to any of the values.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="values">The values.</param>
        /// <returns>Returs a list of all characters.</returns>
        public static int[] AllIndexesOf(this string str, char[] values)
        {
            int[] maxList = new int[str.Length];
            int listIndex = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                if (EqualsAny(str[i], values))
                {
                    maxList[listIndex++] = i;
                }
            }

            int[] resList = new int[listIndex];

            for (int i = 0; i < listIndex; ++i)
            {
                resList[i] = maxList[i];
            }

            return resList;
        }

        /// <summary>
        /// Checks if any character equals the given character.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <param name="values">The character to check with.</param>
        /// <returns>True when any is equal, else Fals.</returns>
        private static bool EqualsAny(char c, char[] values)
        {
            foreach (var value in values)
            {
                if (c == value)
                    return true;
            }

            return false;
        }
    }
}
