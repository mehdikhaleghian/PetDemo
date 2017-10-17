using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Service.PeopleManagerTests
{
    public class GetPeopleStatusCodeOrientedTests : PeopleServiceTestsBase
    {
        [Test]
        public void GetPeople_Should_Throw_TimeoutException_If_Getting_People_RequestTimesout()
        {
            SetupHttpHandler(HttpStatusCode.RequestTimeout, Resources.PeopleJson);
            var aggregateException = Assert.Throws<AggregateException>(() => Sut.GetPeople());
            aggregateException.InnerExceptions.Count(x => x.GetType() == typeof(TimeoutException))
                .Should().Be(1);
        }

        [Test]
        public void GetPeople_Should_Throw_TimeoutException_If_Getting_People_GateWayTimesout()
        {
            SetupHttpHandler(HttpStatusCode.GatewayTimeout, Resources.PeopleJson);
            var aggregateException = Assert.Throws<AggregateException>(() => Sut.GetPeople());
            aggregateException.InnerExceptions.Count(x => x.GetType() == typeof(TimeoutException))
                .Should().Be(1);
        }

        [Test]
        public void GetPeople_Should_Throw_InvalidOperationException_If_Getting_People_Returns_404()
        {
            SetupHttpHandler(HttpStatusCode.NotFound, Resources.PeopleJson);
            var aggregateException = Assert.Throws<AggregateException>(() => Sut.GetPeople());
            aggregateException.InnerExceptions.Count(x => x.GetType() == typeof(InvalidOperationException))
                .Should().Be(1);
        }

        /// <summary>
        /// this method tests a bunch of unexpected status codes
        /// </summary>
        /// <param name="statusCode"></param>
        [Test]
        [TestCase(HttpStatusCode.Ambiguous)]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.Forbidden)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.MethodNotAllowed)]
        [TestCase(HttpStatusCode.Redirect)]
        public void GetPeople_Should_Throw_ApplicationException_If_Getting_People_Returns_Status_Code(HttpStatusCode statusCode)
        {
            SetupHttpHandler(statusCode, Resources.PeopleJson);
            var aggregateException = Assert.Throws<AggregateException>(() => Sut.GetPeople());
            aggregateException.InnerExceptions.Count(x => x.GetType() == typeof(ApplicationException))
                .Should().Be(1);
        }
    }
}