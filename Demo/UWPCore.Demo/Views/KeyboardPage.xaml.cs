using UWPCore.Framework.Controls;
using UWPCore.Framework.Input;
using UWPCore.Framework.UI;
using Windows.System;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KeyboardPage : UniversalPage
    {
        private IKeyboardService _keyboardService;
        private IDialogService _dialogService;

        public KeyboardPage()
        {
            InitializeComponent();
            _dialogService = Injector.Get<IDialogService>();

            _keyboardService = Injector.Get<IKeyboardService>();
            RegisterForKeyboard();
        }

        public override void OnResume()
        {
            base.OnResume();

            RegisterForKeyboard();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            _keyboardService.UnregisterForKeyDown();
        }

        private void RegisterForKeyboard()
        {
            _keyboardService.RegisterForKeyDown(async (e) =>
            {
                if (e.VirtualKey == VirtualKey.Enter)
                {
                    await _dialogService.ShowAsync("ENTER was pressed", "Info");
                }
                else if (e.ControlKey && e.VirtualKey == VirtualKey.X)
                {
                    await _dialogService.ShowAsync("CTRL+X was pressed", "Info");
                }
                else if (e.VirtualKey == VirtualKey.X)
                {
                    await _dialogService.ShowAsync("X was pressed", "Info");
                }
            });
        }
    }
}
