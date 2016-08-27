using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.Data;
using UWPCore.Framework.Mvvm;
using UWPCore.Framework.Storage;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class SettingsWithStringSourceViewModel : ViewModelBase
    {
        public EnumSource<SettingsEnum> ComboBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public EnumSource<SettingsEnum> ListBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public RadioButtonStringSource RadioButtonSource { get; private set; }

        public RadioButtonStringSource RadioButtonThemeSource { get; private set; }

        public StringComboBoxSource TestStringSource { get; private set; }

        private Localizer _localizer = new Localizer();

        public static StoredObjectBase<string> TestSettings = new LocalObject<string>("StringSource_TestValue", "TEST_A");

        public SettingsWithStringSourceViewModel()
        {
            // localize string source
            TestStringSource = new StringComboBoxSource(new List<SourceComboBoxItem>(){
                new SourceComboBoxItem("TEST_A", _localizer.Get("StringSource.TextA")),
                new SourceComboBoxItem("TEST_B", _localizer.Get("StringSource.TextB"))
            });
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);

            ComboBoxEnumSource.SelectedValue = AppSettings.SettingsSampleComboBoxEnum.Value;
            ListBoxEnumSource.SelectedValue = AppSettings.SettingsSampleListBoxEnum.Value;

            TestStringSource.SelectedValue = TestSettings.Value;
        }

        public async override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            await base.OnNavigatedFromAsync(state, suspending);

            AppSettings.SettingsSampleComboBoxEnum.Value = ComboBoxEnumSource.SelectedValue;
            AppSettings.SettingsSampleListBoxEnum.Value = ListBoxEnumSource.SelectedValue;

            TestSettings.Value = TestStringSource.SelectedValue;

            UniversalApp.Current.UpdateTheme();
        }

        public bool SampleBoolean
        {
            get
            {
                return AppSettings.SettingsSampleBoolean.Value;
            }
            set
            {
                AppSettings.SettingsSampleBoolean.Value = value;
            }
        }

        public int SampleInteger
        {
            get
            {
                return AppSettings.SettingsSampleInteger.Value;
            }
            set
            {
                AppSettings.SettingsSampleInteger.Value = value;
            }
        }

        public bool? SampleNullableBoolean
        {
            get
            {
                return AppSettings.SettingsSampleNullableBoolean.Value;
            }
            set
            {
                AppSettings.SettingsSampleNullableBoolean.Value = value;
            }
        }
    }
}
