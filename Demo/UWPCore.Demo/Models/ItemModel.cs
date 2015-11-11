using System.Runtime.Serialization;
using UWPCore.Framework.Mvvm;

namespace UWPCore.Demo.Models
{
    [DataContract]
    public class ItemModel : BindableBase
    {
        private int _id;
        [DataMember]
        public int Id { get { return _id; } set { Set(ref _id, value); } }

        private string _title;
        [DataMember]
        public string Title { get { return _title; } set { Set(ref _title, value); } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { Set(ref _description, value); } }

        private SubItemModel _subItem;
        [DataMember]
        public SubItemModel SubItem { get { return _subItem; } set { Set(ref _subItem, value); } }

        public ItemModel() { }

        public ItemModel(ItemModel item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            if (item.SubItem != null)
                SubItem = new SubItemModel(item.SubItem);
        }

        public ItemModel(int id, string name, string description, SubItemModel subItem)
        {
            Id = id;
            Title = name;
            Description = description;
            SubItem = subItem;
        }
    }
}
