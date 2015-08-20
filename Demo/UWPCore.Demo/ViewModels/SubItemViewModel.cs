using UWPCore.Demo.Models;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Demo.ViewModels
{
    public class SubItemViewModel : ViewModelBase
    {
        public SubItemModel Model { get; private set; }

        public SubItemViewModel(SubItemModel subItem)
        {
            Model = new SubItemModel(subItem);
        }
    }
}