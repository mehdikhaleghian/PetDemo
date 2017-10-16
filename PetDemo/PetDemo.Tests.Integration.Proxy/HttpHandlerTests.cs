using System;
using System.Net.Http;
using System.Net.Http.Headers;
using FluentAssert;
using NUnit.Framework;
using PetDemo.Proxy;

namespace PetDemo.IntegrationTests.Proxy
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
        public void GetAsync_Should_Return_Expetected_Json_Result()
        {
            var jsonContent = _sut.GetAsync("people").Result.Content.ReadAsStringAsync().Result;
            jsonContent.ShouldBeEqualTo("");
        }
    }
}
