using System;
using System.Net.Http;
using System.Net.Http.Headers;
using FluentAssertions;
using NUnit.Framework;
using PetDemo.Proxy;
using PetDemo.Tests.Common;

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
        public void GetAsync_Should_Return_Expected_JsonString()
        {
            var expected = ResourceReader.ReadAsJsonString(Resources.PeopleJson);
            var t = _sut.GetAsync("people").Result.Content.GetType();
            var actual = _sut.GetAsync("people").Result.Content.ReadAsStringAsync().Result;
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
