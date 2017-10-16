using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PetDemo.Proxy;
using PetDemo.Proxy.Interfaces;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Proxy
{
    [TestFixture]
    public class PeopleManagerTests
    {
        private Mock<IHttpHandler> _mockHttpHandler;
        private PeopleManager _sut;

        [SetUp]
        public void Setup()
        {
            _mockHttpHandler = new Mock<IHttpHandler>();
            _sut = new PeopleManager(_mockHttpHandler.Object);
        }

        [Test]
        public void GetPeople_Should_Return_The_List_Of_People_Correctly()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = _sut.GetPeople();
            people.Should().NotBeNull();
            people.Length.Should().Be(6);
            people.First(x => x.Name == "Alice").Pets.Count().Should().Be(2);
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }

        [Test]
        public void GetPeople_Should_Throw_TimeoutException_If_Getting_People_Timesout()
        {
            SetupHttpHandler(HttpStatusCode.RequestTimeout);
            var aggregateException = Assert.Throws<AggregateException>(() => _sut.GetPeople());
            aggregateException.InnerExceptions.Count(x => x.GetType() == typeof(TimeoutException))
                .Should().Be(1);
        }

        private void SetupHttpHandler(HttpStatusCode statusCode)
        {

            var testContent = ResourceReader.ReadAsJsonString(Resources.PeopleJson);
            var responseMessage = Task.FromResult(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(testContent)
            });
            _mockHttpHandler.Setup(x => x.GetAsync(It.Is<string>(url => url == "people")))
                .Returns(responseMessage);
        }
    }
}
