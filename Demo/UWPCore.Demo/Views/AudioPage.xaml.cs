using UWPCore.Framework.Audio;
using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using Windows.UI.Xaml;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AudioPage : UniversalPage
    {
        private IAudioService _audioService;

        public AudioPage()
        {
            InitializeComponent();

            _audioService = UniversalApp.Injector.Get<IAudioService>();
        }

        private async void PlayMp3Clicked(object sender, RoutedEventArgs e)
        {
            await _audioService.PlayAsync("/Assets/Audio/bing.mp3");
        }

        private async void PlayWavClicked(object sender, RoutedEventArgs e)
        {
            await _audioService.PlayAsync("/Assets/Audio/click.wav");
        }

        private void StopClicked(object sender, RoutedEventArgs e)
        {
            _audioService.Stop();
        }
    }
}
