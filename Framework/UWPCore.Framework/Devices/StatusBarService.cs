using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace UWPCore.Framework.Devices
{
    /// <summary>
    /// Service class to access the status bar, which is only available in Windows 10 Mobile.
    /// </summary>
    public class StatusBarService : IStatusBarService
    {
        /// <summary>
        /// The status bar.
        /// </summary>
        private StatusBar _statusBar;

        /// <summary>
        /// Creates a StatusBarService instance.
        /// Remember: Do NOT create this service in any constructor, which will lead to a AccessViolationException.
        /// </summary>
        public StatusBarService()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                _statusBar = StatusBar.GetForCurrentView();
            }
        }

        public async Task ShowAsync()
        {
            if (!IsSupported)
                return;

            await _statusBar.ShowAsync();
        }

        public async Task HideAsync()
        {
            if (!IsSupported)
                return;

            await _statusBar.HideAsync();
        }

        public async Task StartProgressAsync(string text, bool isIntermediate = false)
        {
            if (!IsSupported)
                return;

            _statusBar.ProgressIndicator.Text = text;
            _statusBar.ProgressIndicator.ProgressValue = (isIntermediate) ? null : new double?(0);
            await _statusBar.ProgressIndicator.ShowAsync();
        }

        public void UpdateProgress(double? value, string text = null)
        {
            if (!IsSupported)
                return;

            // ensure value in range
            if (value.HasValue)
            {
                if (value < 0)
                    value = 0;
                else if (value > 1)
                    value = 1;
            }

            _statusBar.ProgressIndicator.ProgressValue = value;

            if (text != null)
                _statusBar.ProgressIndicator.Text = text;
        }

        public async Task StopProgressAsync()
        {
            if (!IsSupported)
                return;

            _statusBar.ProgressIndicator.Text = string.Empty;
            _statusBar.ProgressIndicator.ProgressValue = null;
            await _statusBar.ProgressIndicator.HideAsync();
        }

        public bool IsSupported
        {
            get
            {
                return _statusBar != null;
            }
        }

        public Color ForegroundColor
        {
            set
            {
                if (!IsSupported)
                    return;

                _statusBar.ForegroundColor = value;
            }
            get
            {
                return _statusBar.ForegroundColor.HasValue ? _statusBar.ForegroundColor.Value : Colors.Transparent;
            }
        }

        public Color BackgroundColor
        {
            set
            {
                if (!IsSupported)
                    return;

                // use opacity from background color, because it is transparent by default
                _statusBar.BackgroundOpacity = (value.A / 255.0);
                _statusBar.BackgroundColor = value;
            }
            get
            {
                return _statusBar.BackgroundColor.HasValue ? _statusBar.BackgroundColor.Value : Colors.Transparent;
            }
        }
    }
}
