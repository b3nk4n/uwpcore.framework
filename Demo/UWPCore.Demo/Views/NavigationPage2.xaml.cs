using UWPCore.Framework.Controls;
using UWPCore.Framework.Logging;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationPage2 : UniversalPage
    {
        public NavigationPage2()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Logger.WriteLine("PAGE - OnNavigatedTo");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Logger.WriteLine("PAGE - OnNavigatingFrom");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Logger.WriteLine("PAGE - OnNavigatedFrom");
        }
    }
}
