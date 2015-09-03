using System;
using System.Collections.Generic;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Enum data source to make bingings to enums much more easy.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public sealed class EnumSource<T>
    {
        /// <summary>
        /// Gets or sets the selected enum value.
        /// </summary>
        public object SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets the selected value for storage.
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return SelectedItem.ToString();
            }
        }

        /// <summary>
        /// Gets the enum items as a list.
        /// </summary>
        public IList<T> ItemsSource
        {
            get
            {
                return EnumExtensions.GetAsList<T>();
            }
        }

        /// <summary>
        /// Selects an item from an enum string value.
        /// </summary>
        /// <remarks>
        /// We use string values here, because <see cref="Windows.Storage.ApplicationDataContainer"/>
        /// does not suppoert enum values.
        /// </remarks>
        /// <param name="enumValue">The enum string value.</param>
        public void SelectValue(string enumValue)
        {
            SelectedItem = (T)Enum.Parse(typeof(T), enumValue);
        }
    }
}
