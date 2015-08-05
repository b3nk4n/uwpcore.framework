using Windows.UI.Xaml.Navigation;

namespace UWPCore.Framework.Navigation
{
    public class NavigatingEventArgs : NavigatedEventArgs
    {
        public NavigatingEventArgs() { }
        public NavigatingEventArgs(NavigatingCancelEventArgs e)
        {
            NavigationMode = e.NavigationMode;
            PageType = e.SourcePageType;
            Parameter = e.Parameter?.ToString();
        }
        public bool Cancel { get; set; } = false;
        public bool Suspending { get; set; } = false;
    }
}
