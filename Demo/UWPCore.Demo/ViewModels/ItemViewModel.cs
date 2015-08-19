using System;
using UWPCore.Demo.Models;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Demo.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        public ItemModel Model { get; private set; }

        public ItemViewModel(ItemModel itemModel)
        {
            Model = new ItemModel(itemModel);
        }
    }
}