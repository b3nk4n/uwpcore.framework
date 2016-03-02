using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPCore.Framework.Common;
using UWPCore.Framework.Data;
using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public EnumSource<SettingsEnum> ComboBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public EnumSource<SettingsEnum> ListBoxEnumSource { get; private set; } = new EnumSource<SettingsEnum>();

        public RadioButtonStringSource RadioButtonSource { get; private set; }

        public RadioButtonStringSource RadioButtonThemeSource { get; private set; }

        public SettingsViewModel()
        {

        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            base.OnNavigatedTo(parameter, mode, state);

            ComboBoxEnumSource.SelectedValue = AppSettings.SettingsSampleComboBoxEnum.Value;
            ListBoxEnumSource.SelectedValue = AppSettings.SettingsSampleListBoxEnum.Value;
        }

        public async override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            await base.OnNavigatedFromAsync(state, suspending);

            AppSettings.SettingsSampleComboBoxEnum.Value = ComboBoxEnumSource.SelectedValue;
            AppSettings.SettingsSampleListBoxEnum.Value = ListBoxEnumSource.SelectedValue;

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
