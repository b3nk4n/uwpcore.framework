using System.Collections.Generic;
using System.Linq;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// String data source to make bingings to string much more easy, such as in combo boxes.
    /// </summary>
    public sealed class StringComboBoxSource : BindableBase
    {
        public StringComboBoxSource(IList<SourceComboBoxItem> items)
        {
            _itemsSource = items;
        }

        /// <summary>
        /// Gets or sets the selected string value.
        /// </summary>
        /// <remarks>Must be of type OBJECT to ensure x:Bind is working without a converter.</remarks>
        public object SelectedItem {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }
        private object _selectedItem;

        /// <summary>
        /// Gets or sets the selected value for storage.
        /// Instead of this property, also the property of the model object could be used directly.
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return (SelectedItem as SourceComboBoxItem).Key;
            }
            set
            {
                SelectedItem = ItemsSource.FirstOrDefault(i => i.Key == value);
            }
        }

        /// <summary>
        /// Gets the enum items as a list.
        /// </summary>
        public IList<SourceComboBoxItem> ItemsSource
        {
            get
            {
                return _itemsSource;
            }
        }
        private IList<SourceComboBoxItem> _itemsSource;
    }

    /// <summary>
    /// The used combo box item.
    /// </summary>
    public class SourceComboBoxItem
    {
        public string Key { get; set; }
        public string Translation { get; set; }

        public SourceComboBoxItem(string key, string translation)
        {
            Key = key;
            Translation = translation;
        }
    }
}
