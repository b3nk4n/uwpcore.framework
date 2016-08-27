using UWPCore.Framework.Controls;
using UWPCore.Template.ViewModels;

namespace UWPCore.Template.Views
{
    /// <summary>
    /// The apps main page.
    /// </summary>
    public sealed partial class MainPage : UniversalPage
    {
        /// <summary>
        /// The view model instance.
        /// </summary>
        public MainViewModel ViewModel { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }
    }
}
