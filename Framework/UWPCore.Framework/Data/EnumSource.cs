using System;
using System.Collections.Generic;
using UWPCore.Framework.Common;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Enum data source to make bingings to enums much more easy.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public sealed class EnumSource<T> where T : struct, IConvertible
    {
        /// <summary>
        /// Gets or sets the selected enum value.
        /// </summary>
        public object SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets the selected value for storage.
        /// </summary>
        /// <remarks>
        /// We use string values here, because <see cref="Windows.Storage.ApplicationDataContainer"/>
        /// does not suppoert enum values.
        /// </remarks>
        public string SelectedValue
        {
            get
            {
                return SelectedItem.ToString();
            }
            set
            {
                SelectedItem = (T)Enum.Parse(typeof(T), value);
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
    }
}
