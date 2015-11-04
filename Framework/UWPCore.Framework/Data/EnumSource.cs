using System;
using System.Collections.Generic;
using UWPCore.Framework.Common;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Enum data source to make bingings to enums much more easy.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public sealed class EnumSource<T> : BindableBase where T : struct, IConvertible
    {
        /// <summary>
        /// Gets or sets the selected enum value.
        /// </summary>
        public object SelectedItem {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }
        private object _selectedItem;

        /// <summary>
        /// Gets or sets the selected value for storage.
        /// Instead of this property, also the property of the model object could be used directly.
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
