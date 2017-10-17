using Newtonsoft.Json;
using NUnit.Framework;
using PetDemo.Model;
using PetDemo.Service;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Service.JsonDeserializerTests
{
    [TestFixture]
    public class JsonDeserilizerTests
    {
        private JsonDeserializer<Person[]> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new JsonDeserializer<Person[]>();
        }

        [Test]
        public void Deserialize_Should_Throw_JsonException_If_The_Given_Input_Does_Not_Have_Valid_Schema()
        {
            var content = EmbeddedResourceReader.ReadAsJsonString(Resources.InvalidJson);
            Assert.Throws<JsonException>(() => _sut.Deserialize(content));
        }
    }
}
