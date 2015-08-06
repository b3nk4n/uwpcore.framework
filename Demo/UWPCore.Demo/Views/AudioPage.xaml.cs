using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore.Framework.Audio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AudioPage : Page
    {
        private IAudioService _audioService;

        public AudioPage()
        {
            InitializeComponent();

            _audioService = new AudioService();
            _audioService.Register(this);
        }

        private void PlayMp3Clicked(object sender, RoutedEventArgs e)
        {
            _audioService.Play("Assets/Audio/bing.mp3");
        }

        private void PlayWavClicked(object sender, RoutedEventArgs e)
        {
            _audioService.Play("Assets/Audio/click.wav");
        }

        private void StopClicked(object sender, RoutedEventArgs e)
        {
            _audioService.Stop();
        }
    }
}
