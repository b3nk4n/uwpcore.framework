using System;
using System.Globalization;
using Windows.UI;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// Conversion methods from color.
    /// </summary>
    public static class ColorConverter
    {
        /// <summary>
        /// Converts a given hex string to a color.
        /// </summary>
        /// <param name="hex">
        /// The hex string in formats such as #aarrggbb, #rrggbb, #argb, #rgb
        /// or even without hash symbol.
        /// </param>
        /// <returns>The converted color</returns>
        public static Color FromHex(string hex)
        {
            // trim hash sign
            string hexString;
            if (hex[0] == '#')
                hexString = hex.Substring(1, hex.Length - 1);
            else
                hexString = hex;

            var color = new Color();
            int a, r, g, b;
            switch(hexString.Length)
            {
                case 3: // #rgb
                    r = int.Parse(hexString.Substring(0, 1), NumberStyles.AllowHexSpecifier);
                    g = int.Parse(hexString.Substring(1, 1), NumberStyles.AllowHexSpecifier);
                    b = int.Parse(hexString.Substring(2, 1), NumberStyles.AllowHexSpecifier);
                    color.A = 255;
                    color.R = (byte)(r * 16 + r);
                    color.G = (byte)(g * 16 + g);
                    color.B = (byte)(b * 16 + b);
                    break;

                case 4: // #argb
                    a = int.Parse(hexString.Substring(0, 1), NumberStyles.AllowHexSpecifier);
                    r = int.Parse(hexString.Substring(1, 1), NumberStyles.AllowHexSpecifier);
                    g = int.Parse(hexString.Substring(2, 1), NumberStyles.AllowHexSpecifier);
                    b = int.Parse(hexString.Substring(3, 1), NumberStyles.AllowHexSpecifier);

                    color.A = (byte)(a * 16 + a);
                    color.R = (byte)(r * 16 + r);
                    color.G = (byte)(g * 16 + g);
                    color.B = (byte)(b * 16 + b);
                    break;

                case 6: // #rrggbb
                    color.A = 255;
                    color.R = byte.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    color.G = byte.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    color.B = byte.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    break;

                case 8: // #aarrggbb
                    color.A = byte.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    color.R = byte.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    color.G = byte.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    color.B = byte.Parse(hexString.Substring(6, 2), NumberStyles.AllowHexSpecifier);
                    break;

                default:
                    throw new ArgumentException("Invalid color hex code length.");
            }

            return color;
        }
    }
}
