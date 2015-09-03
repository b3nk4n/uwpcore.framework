using UWPCore.Framework.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public EnumSource<SettingsEnum> EnumSource { get; private set; } = new EnumSource<SettingsEnum>(); // TODO: possible to use in other control than combobox? Rename to ComboBox(Enum)Source?

        public RadioButtonSource<string> RadioButtonSource { get; private set; } // TODO: possible to use other type than string???

        public SettingsPage()
        {
            InitializeComponent();

            RadioButtonSource = new RadioButtonSource<string>(RadioButtonContainer);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SampleToggleSwitch.IsOn = AppSettings.SettingsSampleBoolean.Value;
            EnumSource.SelectedValue = AppSettings.SettingsSampleEnum.Value;
            SampleSlider.Value = AppSettings.SettingsSampleInteger.Value;
            SampleCheckBox.IsChecked = AppSettings.SettingsSampleNullableBoolean.Value;
            RadioButtonSource.SelectedValue = AppSettings.SettingsSampleString.Value;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            AppSettings.SettingsSampleBoolean.Value = SampleToggleSwitch.IsOn;
            AppSettings.SettingsSampleEnum.Value = EnumSource.SelectedValue;
            AppSettings.SettingsSampleInteger.Value = (int)SampleSlider.Value;
            AppSettings.SettingsSampleNullableBoolean.Value = SampleCheckBox.IsChecked;
            AppSettings.SettingsSampleString.Value = RadioButtonSource.SelectedValue;
        }

        
    }
}
