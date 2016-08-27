using UWPCore.Framework.Controls;
using UWPCore.Template.ViewModels;

namespace UWPCore.Template.Views
{
    /// <summary>
    /// The settings page.
    /// </summary>
    public sealed partial class SettingsPage : UniversalPage
    {
        /// <summary>
        /// The view model instance.
        /// </summary>
        public SettingsViewModel ViewModel { get; private set; }

        public SettingsPage()
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel();
            DataContext = ViewModel;
        }
    }
}
