using System;
using System.Runtime.Serialization;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Demo.Models
{
    [DataContract]
    public class SubItemModel : BindableBase
    {
        private string _title;
        [DataMember]
        public string Title { get { return _title; } set { Set(ref _title, value); } }

        private Uri _link;
        [DataMember]
        public Uri Link { get { return _link; } set { Set(ref _link, value); } }

        public SubItemModel() { }

        public SubItemModel(SubItemModel subItem)
        {
            Title = subItem.Title;
            Link = subItem.Link;
        }

        public SubItemModel(string title, Uri link)
        {
            Title = title;
            Link = link;
        }
    }
}
