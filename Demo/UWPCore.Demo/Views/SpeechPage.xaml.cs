using UWPCore.Framework.Controls;
using UWPCore.Framework.Speech;
using Windows.UI.Xaml;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpeechPage : UniversalPage
    {
        private ISpeechService _speechService;

        public SpeechPage()
        {
            InitializeComponent();

            _speechService = new SpeechService();
        }

        private async void SpeakClicked(object sender, RoutedEventArgs e)
        {
            await _speechService.SpeakTextAsync(SpeakTextBox.Text);
        }

        private async void RecognizeUIClicked(object sender, RoutedEventArgs e)
        {
            var result = await _speechService.RecoginizeUI();
            if (result != null)
            {
                RecognizeTextBox.Text = result.Text;
            }
        }
    }
}
