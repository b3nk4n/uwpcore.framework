using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UWPCore.Demo.Models;
using UWPCore.Framework.Common;
using UWPCore.Framework.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.ViewModels
{
    public class MvvmViewModel : ViewModelBase
    {
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public DelegateCommand<ItemModel> AddItemCommand { get; private set; }

        public ICommand GoHomeCommand { get; private set; }

        public DelegateCommand<int> DeleteItemCommand { get; private set; }

        public MvvmViewModel()
        {
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
            
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {


            return base.OnNavigatedFromAsync(state, suspending);
        }
    }
}
