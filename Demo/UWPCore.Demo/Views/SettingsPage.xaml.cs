using UWPCore.Framework.Common;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Data;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : UniversalPage
    {
        public EnumSource<SettingsEnum> ComboBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public EnumSource<SettingsEnum> ListBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public RadioButtonStringSource RadioButtonSource { get; private set; }

        public RadioButtonStringSource RadioButtonThemeSource { get; private set; }

        public SettingsPage()
        {
            InitializeComponent();

            RadioButtonSource = new RadioButtonStringSource(RadioButtonContainer);
            RadioButtonThemeSource = new RadioButtonStringSource(ThemeContainer);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SampleToggleSwitch.IsOn = AppSettings.SettingsSampleBoolean.Value;
            ComboBoxEnumSource.SelectedValue = AppSettings.SettingsSampleComboBoxEnum.Value;
            ListBoxEnumSource.SelectedValue = AppSettings.SettingsSampleListBoxEnum.Value;
            SampleSlider.Value = AppSettings.SettingsSampleInteger.Value;
            SampleCheckBox.IsChecked = AppSettings.SettingsSampleNullableBoolean.Value;
            RadioButtonSource.SelectedValue = AppSettings.SettingsSampleString.Value;

            RadioButtonThemeSource.SelectedValue = PageTheme.Value;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            AppSettings.SettingsSampleBoolean.Value = SampleToggleSwitch.IsOn;
            AppSettings.SettingsSampleComboBoxEnum.Value = ComboBoxEnumSource.SelectedValue;
            AppSettings.SettingsSampleListBoxEnum.Value = ListBoxEnumSource.SelectedValue;
            AppSettings.SettingsSampleInteger.Value = (int)SampleSlider.Value;
            AppSettings.SettingsSampleNullableBoolean.Value = SampleCheckBox.IsChecked;
            AppSettings.SettingsSampleString.Value = RadioButtonSource.SelectedValue;

            PageTheme.Value = RadioButtonThemeSource.SelectedValue;

            UniversalApp.Current.UpdateTheme();
        }
     
    }
}
