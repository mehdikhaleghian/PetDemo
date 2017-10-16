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
        public void GetPeople_Should_Not_Return_Null_When_HttpHandler_Returns_OK_Response()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = _sut.GetPeople();
            people.Should().NotBeNull();
        }

        [Test]
        public void GetPeople_Should_Return_The_Correct_Number_Of_Results_Which_Is_Retrieved_From_API()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = _sut.GetPeople();
            people.Length.Should().Be(6);
        }

        [Test]
        public void GetPeople_Should_Return_The_Count_Of_Pets_Correctly()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = _sut.GetPeople();
            people.First(x => x.Name == "Alice").Pets.Count().Should().Be(2);
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }

        [Test]
        public void GetPeople_Should_Return_Null_When_Pets_Is_Set_As_Null()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = _sut.GetPeople();
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

        /// <summary>
        /// This method sets up the IHttpHandler with the specified http status code and results from the test json file
        /// </summary>
        /// <param name="statusCode">the status code returned by the calling the handler</param>
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
