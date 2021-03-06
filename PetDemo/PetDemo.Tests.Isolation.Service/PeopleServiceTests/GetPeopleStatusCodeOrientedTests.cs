using System;
using System.Net;
using NUnit.Framework;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Service.PeopleServiceTests
{
    public class GetPeopleStatusCodeOrientedTests : PeopleServiceTestsBase
    {
        [Test]
        public void GetPeopleAsync_Should_Throw_TimeoutException_If_Getting_People_RequestTimesout()
        {
            SetupHttpHandler(HttpStatusCode.RequestTimeout, Resources.PeopleJson);
            Assert.ThrowsAsync<TimeoutException>(async () => await Sut.GetPeopleAsync());
        }

        [Test]
        public void GetPeopleAsync_Should_Throw_TimeoutException_If_Getting_People_GateWayTimesout()
        {
            SetupHttpHandler(HttpStatusCode.GatewayTimeout, Resources.PeopleJson);
            Assert.ThrowsAsync<TimeoutException>(async () => await Sut.GetPeopleAsync());
        }

        [Test]
        public void GetPeopleAsync_Should_Throw_InvalidOperationException_If_Getting_People_Returns_404()
        {
            SetupHttpHandler(HttpStatusCode.NotFound, Resources.PeopleJson);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await Sut.GetPeopleAsync());
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
        public void GetPeopleAsync_Should_Throw_ApplicationException_If_Getting_People_Returns_Status_Code(HttpStatusCode statusCode)
        {
            SetupHttpHandler(statusCode, Resources.PeopleJson);
            Assert.ThrowsAsync<ApplicationException>(() => Sut.GetPeopleAsync());
        }
    }
}