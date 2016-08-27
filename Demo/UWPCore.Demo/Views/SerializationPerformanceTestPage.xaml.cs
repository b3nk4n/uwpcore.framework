using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using UWPCore.Framework.Controls;
using UWPCore.Framework.Data;
using UWPCore.Framework.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SerializationPerformanceTestPage : UniversalPage
    {
        private ISerializationService SerializationService { get; set; }
        private IStorageService StorageService { get; set; }

        public SerializationPerformanceTestPage()
        {
            InitializeComponent();

            SerializationService = Injector.Get<ISerializationService>();
            StorageService = Injector.Get<ILocalStorageService>();
        }

        private void SerializeDataContractJsonClicked(object sender, RoutedEventArgs e)
        {
            IList<RootObject> list = new List<RootObject>();
            list.Add(GetObject("Jan", "Munich"));
            list.Add(GetObject("Peter", "Munster"));
            list.Add(GetObject("Karl", "Köln"));
            list.Add(GetObject("Hugo", "Paris"));
            list.Add(GetObject("Luis", "London"));

            var start = DateTime.Now;

            var data = SerializationService.SerializeJson(list);

            var end = DateTime.Now;

            Output.Text = string.Format("{0} ticks ({1} sec)", end.Ticks - start.Ticks, (end - start).TotalSeconds);
        }

        private async void DeserializeDataContractJsonClicked(object sender, RoutedEventArgs e)
        {
            var file = await StorageService.GetFileFromApplicationAsync("/Assets/Test/serialization.json");
            var jsonString = await StorageService.ReadFile(file);

            if (jsonString != null)
            {
                var start = DateTime.Now;

                var data = SerializationService.DeserializeJson<IList<RootObject>>(jsonString);

                var end = DateTime.Now;

                Output.Text = string.Format("{0} ticks ({1} sec)\ndata-count: {2}", end.Ticks - start.Ticks, (end - start).TotalSeconds, data.Count);
            }
        }

        private RootObject GetObject(string first, string city)
        {
            var obj = new RootObject(first, "Huber", 21);
            obj.Address = new Address("Steet 1", city, "BY", "12345");
            obj.PhoneNumber.Add(new PhoneNumber("Primary", "131-1231312-12321"));
            obj.PhoneNumber.Add(new PhoneNumber("Secondary", "312-1513-1231"));
            return obj;
        }
    }

    [DataContract]
    public class Address
    {
        [DataMember(Name = "streetAddress")]
        public string StreetAddress { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        public Address(string street, string city, string state, string postal)
        {
            StreetAddress = street;
            City = city;
            State = state;
            PostalCode = postal;
        }
    }

    [DataContract]
    public class PhoneNumber
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "number")]
        public string Number { get; set; }

        public PhoneNumber(string type, string number)
        {
            Type = type;
            Number = number;
        }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "age")]
        public int Age { get; set; }
        [DataMember(Name = "address")]
        public Address Address { get; set; }
        [DataMember(Name = "phoneNumber")]
        public List<PhoneNumber> PhoneNumber { get; set; } = new List<Views.PhoneNumber>();

        public RootObject(string first, string last, int age)
        {
            FirstName = first;
            LastName = last;
            Age = age;
        }
    }
}
