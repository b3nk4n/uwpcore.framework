using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPCore.Demo.Models;
using UWPCore.Framework.Common;
using UWPCore.Framework.Data;
using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class MvvmViewModel : ViewModelBase
    {
        private const string TEMPORARY_ITEM_KEY = "tempItem";

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public DelegateCommand<ItemModel> AddItemCommand { get; private set; }

        public ICommand GoHomeCommand { get; private set; }

        public DelegateCommand<int> DeleteItemCommand { get; private set; }

        private ISerializationService _serializationService;

        /// <summary>
        /// The temporary item view model that we actively work on befor adding it to the list.
        /// </summary>
        public ItemViewModel TemporaryItem { get; private set; } = new ItemViewModel(new ItemModel());

        public MvvmViewModel()
        {
            _serializationService = new DataContractSerializationService();
                 
            Items = new ObservableCollection<ItemViewModel>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddItemCommand = new DelegateCommand<ItemModel>((item) =>
            {
                Items.Add(new ItemViewModel(item));
                DeleteItemCommand.RaiseCanExecuteChanged();
            });

            DeleteItemCommand = new DelegateCommand<int>((int index) =>
            {
                Items.RemoveAt(index);
            },
            (int index) =>
            {
                if (index == -1)
                    return false;

                return Items.Count > 0;
            });

            GoHomeCommand = new DelegateCommand(() =>
            {
                NavigationService.Navigate((Application.Current as UniversalApp).DefaultPage);
            });
        }

        public override void OnNavigatedTo(string parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(TEMPORARY_ITEM_KEY))
            {
                var deserializedTempItem = _serializationService.DeserializeJson<ItemModel>(state[TEMPORARY_ITEM_KEY] as string); // FIXME: deserialization/ser. done impl. ???
                TemporaryItem = new ItemViewModel(deserializedTempItem);
            }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            var serializedTempItem = _serializationService.SerializeJson(TemporaryItem.Model);
            // TODO: dictionary extension: AddOrReplace(...)
            if (!state.ContainsKey(TEMPORARY_ITEM_KEY))
            {
                state.Add(TEMPORARY_ITEM_KEY, serializedTempItem);
            } 
            else
            {
                state[TEMPORARY_ITEM_KEY] = serializedTempItem;
            }

            return base.OnNavigatedFromAsync(state, suspending);
        }
    }
}
