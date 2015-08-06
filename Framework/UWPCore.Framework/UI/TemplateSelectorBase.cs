using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.UI
{
    /// <summary>
    /// Base template selector class to allow dynamic <see cref="DataTemplate"/> selection.
    /// </summary>
    public abstract class TemplateSelectorBase : ContentControl
    {
        /// <summary>
        /// Selects a data template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        /// <summary>
        /// The on content changed hook method to react on content changed.
        /// </summary>
        /// <param name="oldContent">The old content.</param>
        /// <param name="newContent">The new content.</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            ContentTemplate = SelectTemplate(newContent, this);
        }
    }

}
