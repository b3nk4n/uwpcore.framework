using System;
using System.Collections.Generic;
using UWPCore.Framework.Common;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Enum data source to make bingings to enums much more easy,
    /// where the prefered <see cref="EnumSource{T}"/> is not possible.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public sealed class EnumIndexSource<T> : BindableBase where T : struct, IConvertible
    {
        /// <summary>
        /// Gets or sets the selected enum value.
        /// </summary>
        public int SelectedIndex {
            get { return _selectedIndex; }
            set { Set(ref _selectedIndex, value); }
        }
        private int _selectedIndex;

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
                T enumValue = (T)Enum.ToObject(typeof(T), SelectedIndex);
                return enumValue.ToString();
            }
            set
            {
                T enumValue = (T)Enum.Parse(typeof(T), value);
                int val = (int)Convert.ChangeType(enumValue, typeof(int));
                SelectedIndex = val;
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
