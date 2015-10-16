using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Runtime.Serialization;

namespace UWPCore.Framework.Data
{
    [TestClass]
    public class DataContractSerializationServiceTest
    {
        /// <summary>
        /// The system under test.
        /// </summary>
        private ISerializationService _serializationService;

        private Person _testPerson1 = new Person("Hans", 25, new Address("Munich", "Mainstreet", 1));

        [TestInitialize]
        public void Initialize()
        {
            _serializationService = new DataContractSerializationService();
        }

        [TestMethod]
        public void TestSerializeToJsonString()
        {
            string serializedPerson = _serializationService.SerializeJson(_testPerson1);
            Assert.IsNotNull(serializedPerson);
            Assert.AreEqual("{\"Address\":{\"City\":\"Munich\",\"Street\":\"Mainstreet\",\"StreetNumber\":1},\"Age\":25,\"Name\":\"Hans\"}", serializedPerson);
        }

        [TestMethod]
        public void TestSerializeToXmlString()
        {
            string serializedPerson = _serializationService.SerializeXML(_testPerson1);
            Assert.IsNotNull(serializedPerson);
            Assert.AreEqual("<DataContractSerializationServiceTest.Person xmlns=\"http://schemas.datacontract.org/2004/07/UWPCore.Framework.Data\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">"
                + "<Address>"
                + "<City>Munich</City>"
                + "<Street>Mainstreet</Street>"
                + "<StreetNumber>1</StreetNumber>"
                + "</Address>"
                + "<Age>25</Age>"
                + "<Name>Hans</Name>"
                + "</DataContractSerializationServiceTest.Person>",
                serializedPerson);
        }

        [TestMethod]
        public void TestSerializeDeserializeXmlString()
        {
            string serializedPerson = _serializationService.SerializeXML(_testPerson1);
            Assert.IsNotNull(serializedPerson);
            var deserializedPerson = _serializationService.DeserializeXML<Person>(serializedPerson);
            Assert.AreEqual(_testPerson1.Age, deserializedPerson.Age);
            Assert.AreEqual(_testPerson1.Name, deserializedPerson.Name);
            Assert.AreEqual(_testPerson1.Address.City, deserializedPerson.Address.City);
            Assert.AreEqual(_testPerson1.Address.Street, deserializedPerson.Address.Street);
            Assert.AreEqual(_testPerson1.Address.StreetNumber, deserializedPerson.Address.StreetNumber);
        }

        [TestMethod]
        public void TestSerializeDeserializeJsonString()
        {
            string serializedPerson = _serializationService.SerializeJson(_testPerson1);
            Assert.IsNotNull(serializedPerson);
            var deserializedPerson = _serializationService.DeserializeJson<Person>(serializedPerson);
            Assert.AreEqual(_testPerson1.Age, deserializedPerson.Age);
            Assert.AreEqual(_testPerson1.Name, deserializedPerson.Name);
            Assert.AreEqual(_testPerson1.Address.City, deserializedPerson.Address.City);
            Assert.AreEqual(_testPerson1.Address.Street, deserializedPerson.Address.Street);
            Assert.AreEqual(_testPerson1.Address.StreetNumber, deserializedPerson.Address.StreetNumber);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [DataContract]
        class Person
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int Age { get; set; }
            [DataMember]
            public Address Address { get; set; }
            public Person() { }
            public Person(string name, int age, Address address)
            {
                Name = name;
                Age = age;
                Address = address;
            }
        }

        [DataContract]
        class Address
        {
            [DataMember]
            public string City { get; set; }
            [DataMember]
            public string Street { get; set; }
            [DataMember]
            public int StreetNumber { get; set; }
            public Address() { }
            public Address(string city, string street, int streetNumber)
            {
                City = city;
                Street = street;
                StreetNumber = streetNumber;
            }
        }
    }

}
