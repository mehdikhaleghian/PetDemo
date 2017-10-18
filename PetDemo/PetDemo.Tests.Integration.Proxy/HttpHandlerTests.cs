using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using PetDemo.Model;
using PetDemo.Proxy;
using PetDemo.Service;

namespace PetDemo.Tests.Integration.Proxy
{
    [TestFixture]
    public class HttpHandlerTests
    {
        private HttpHandler _sut;

        [SetUp]
        public void Setup()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net/") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _sut = new HttpHandler(client);
        }

        [Test]
        public void GetAsync_Should_Return_JsonString_With_Valid_Schema()
        {
            var actual = _sut.GetAsync("people").Result.Content.ReadAsStringAsync().Result;

            var jsonDeseializer = new JsonDeserializer<Person[]>();
            //json convertion should not throw any exception as internally JsonDeserializer custom code checks for valid schema
            Assert.DoesNotThrow(()=> jsonDeseializer.Deserialize(actual));
        }
    }
}
