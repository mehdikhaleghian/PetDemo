using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PetDemo.Proxy;
using PetDemo.Proxy.Interfaces;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Proxy.PeopleManagerTests
{
    [TestFixture]
    public class PeopleManagerTestsBase
    {
        protected Mock<IHttpHandler> MockHttpHandler;
        protected PeopleManager Sut;

        [SetUp]
        public void Setup()
        {
            MockHttpHandler = new Mock<IHttpHandler>();
            Sut = new PeopleManager(MockHttpHandler.Object);
        }

        /// <summary>
        /// This method sets up the IHttpHandler with the specified http status code and results from the test json file
        /// </summary>
        /// <param name="statusCode">the status code returned by the calling the handler</param>
        protected void SetupHttpHandler(HttpStatusCode statusCode)
        {

            var testContent = ResourceReader.ReadAsJsonString(Resources.PeopleJson);
            var responseMessage = Task.FromResult(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(testContent)
            });
            MockHttpHandler.Setup(x => x.GetAsync(It.Is<string>(url => url == "people")))
                .Returns(responseMessage);
        }
    }
}
