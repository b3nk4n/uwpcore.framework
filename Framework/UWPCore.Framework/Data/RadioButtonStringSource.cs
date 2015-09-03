using System.Linq;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// String data source to select and retrieve RadioButton values of a groupe very easily.
    /// The value of each RadioButton is stored in the <see cref="FrameworkElement.Tag"/>.
    /// </summary>
    public sealed class RadioButtonStringSource
    {
        /// <summary>
        /// The radio button container panel.
        /// </summary>
        private Panel _container;

        /// <summary>
        /// Creates a RadioButtonSource instance.
        /// </summary>
        /// <param name="radioButtonContainer">The radio button container panel.</param>
        public RadioButtonStringSource(Panel radioButtonContainer)
        {
            _container = radioButtonContainer;
        }

        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement.Tag"/> value of the selected radio button.
        /// </summary>
        public string SelectedValue
        {
            get
            {
                var checkedButton = _container.Children.OfType<RadioButton>()
                                                .FirstOrDefault(r => r.IsChecked.Value);
                return (string)checkedButton.Tag;
            }
            set
            {
                var checkedButton = _container.Children.OfType<RadioButton>()
                                                  .FirstOrDefault(r => ((string)r.Tag).Equals(value));
                checkedButton.IsChecked = true;
            }
        }
    }
}
