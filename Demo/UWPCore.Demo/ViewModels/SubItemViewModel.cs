using System;
using UWPCore.Demo.Models;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Demo.ViewModels
{
    public class SubItemViewModel : ViewModelBase
    {
        public SubItemModel Model { get; private set; }

        public SubItemViewModel(SubItemModel subItem)
        {
            Identifier = Guid.NewGuid().ToString();
            Model = new SubItemModel(subItem);
        }

        /*public string Title
        {
            get
            {
                return SubItem.Title;
            }
            set
            {
                if (SubItem.Title != value)
                {
                    SubItem.Title = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Uri Link
        {
            get
            {
                return SubItem.Link;
            }
            set
            {
                if (SubItem.Link != value)
                {
                    SubItem.Link = value;
                    RaisePropertyChanged();
                }
            }
        }*/
    }
}