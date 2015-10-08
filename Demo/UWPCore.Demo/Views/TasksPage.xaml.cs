using UWPCore.Demo.Tasks;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Tasks;
using UWPCore.Framework.UI;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TasksPage : UniversalPage
    {
        private IBackgroundTaskService _backgroundTaskService;
        private IDialogService _dialogService;

        public TasksPage()
        {
            InitializeComponent();
            _backgroundTaskService = Injector.Get<IBackgroundTaskService>();
            _dialogService = Injector.Get<IDialogService>();
        }

        private async void RequestUserAccessClicked(object sender, RoutedEventArgs e)
        {
            bool status = await _backgroundTaskService.RequestAccessAsync();

            await _dialogService.ShowAsync(status.ToString().ToUpper(), "Information");
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

            await _dialogService.ShowAsync(exists.ToString().ToUpper(), "Information");
        }
    }
}
