using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace UWPCore.Framework.UI.Triggers
{
    /// <summary>
    /// A simple orientation state trigger.
    /// </summary>
    /// <remarks>
    /// For more (advanced) triggers, check out: https://github.com/dotMorten/WindowsStateTriggers
    /// PM> Install-Package WindowsStateTriggers
    /// </remarks>
    public class OrientationTrigger : StateTriggerBase
    {
        /// <summary>
        /// Creates an OrientationTrigger instance.
        /// </summary>
        public OrientationTrigger()
        {
            Window.Current.SizeChanged += (s, e) =>
            {
                SetActive(ApplicationView.GetForCurrentView().Orientation.Equals(Orientation));
            };

            SetActive(ApplicationView.GetForCurrentView().Orientation.Equals(Orientation));
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public ApplicationViewOrientation Orientation { get; set; }
    }
}
