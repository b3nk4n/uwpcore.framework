using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace UWPCore.Framework.Common
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the enum values as a list.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>The enum values.</returns>
        public static IList<T> GetAsList<T>()
        {
            var array = Enum.GetValues(typeof(T));
            var list = new List<T>();
            foreach (var item in array)
            {
                list.Add((T)item);
            }
            return list;
        }

        /// <summary>
        /// Gets the enum display values as a list.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>The enum values.</returns>
        public static IList<string> GetDisplayValueList<T>()
        {
            var array = Enum.GetValues(typeof(T));
            var list = new List<string>();
            foreach (var item in array)
            {
                list.Add(((Enum)item).GetDisplayValue());
            }
            return list;
        }

        /// <summary>
        /// Gets the display value that is assigned with the Display attribute via reflection.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>Returns the display value.</returns>
        public static string GetDisplayValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        /// <summary>
        /// Gets the enum value by a given display value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="displayValue">The display value.</param>
        /// <returns>The enum value.</returns>
        public static T GetByDisplayValue<T>(string displayValue)
        {
            var array = Enum.GetValues(typeof(T));
            foreach (var item in array)
            {
                var value = (Enum)item;
                if (value.GetDisplayValue() == displayValue)
                    return (T)item;
            }
            return default(T);
        }

        /// <summary>
        /// Gets the default value that is assigned with the DefaultValue attribute via reflection.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>The enums default value.</returns>
        public static T GetDefaultValue<T>()
        {
            var fields = (typeof(T).GetRuntimeFields());
            var defaultValue = fields.FirstOrDefault(x => x.GetCustomAttribute(typeof(DefaultValueAttribute)) != null);

            if (defaultValue == null)
                return default(T);

            return (T)Enum.Parse(typeof(T), defaultValue.Name);
        }
    }
}
