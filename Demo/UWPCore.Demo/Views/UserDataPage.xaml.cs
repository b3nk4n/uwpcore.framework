using UWPCore.Demo.ViewModels;
using UWPCore.Framework.Controls;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserDataPage : UniversalPage
    {
        #region Fields

        /// <summary>
        /// ViewModel for data binding.
        /// </summary>
        UserDataViewModel viewModel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize object.
        /// </summary>
        public UserDataPage()
        {
            this.InitializeComponent();

            viewModel = new UserDataViewModel();
            this.DataContext = viewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update view if search query changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            await viewModel.SearchForTextAsync(args.QueryText);
        }

        #endregion
    }
}
