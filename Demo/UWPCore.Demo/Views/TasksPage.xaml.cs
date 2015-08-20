using System;
using UWPCore.Demo.Tasks;
using UWPCore.Framework.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TasksPage : Page
    {
        private IBackgroundTaskService _backgroundTaskService;

        public TasksPage()
        {
            InitializeComponent();
            _backgroundTaskService = new BackgroundTaskService();
        }

        private async void RequestUserAccessClicked(object sender, RoutedEventArgs e)
        {
            bool status = await _backgroundTaskService.RequestAccessAsync();

            await new MessageDialog(status.ToString().ToUpper(), "Information").ShowAsync();
        }

        private void RegisterClicked(object sender, RoutedEventArgs e)
        {
            var taskName = TaskNameTextBox.Text;

            _backgroundTaskService.Register(taskName, typeof(SimpleBackgroundTask), new TimeTrigger(15, false));
        }

        private void UnregisterClicked(object sender, RoutedEventArgs e)
        {
            var taskName = TaskNameTextBox.Text;

            _backgroundTaskService.Unregister(taskName);
        }

        private void UnregisterAllClicked(object sender, RoutedEventArgs e)
        {
            _backgroundTaskService.UnregisterAll();
        }

        private async void CheckTaskExistsClicked(object sender, RoutedEventArgs e)
        {
            var taskName = TaskNameTextBox.Text;

            var exists = _backgroundTaskService.RegistrationExists(taskName);

            await new MessageDialog(exists.ToString().ToUpper(), "Information").ShowAsync();
        }
    }
}
