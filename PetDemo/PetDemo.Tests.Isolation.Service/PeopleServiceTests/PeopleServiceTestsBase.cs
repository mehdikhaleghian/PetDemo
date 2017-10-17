using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PetDemo.Proxy.Interfaces;
using PetDemo.Service;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Service.PeopleServiceTests
{
    [TestFixture]
    public class PeopleServiceTestsBase
    {
        protected Mock<IHttpHandler> MockHttpHandler;
        protected PeopleService Sut;

        [SetUp]
        public void Setup()
        {
            MockHttpHandler = new Mock<IHttpHandler>();
            Sut = new PeopleService(MockHttpHandler.Object);
        }

        /// <summary>
        /// This method sets up the IHttpHandler with the specified http status code and results from the test json file
        /// </summary>
        /// <param name="statusCode">the status code returned by the calling the handler</param>
        /// <param name="resourceName">the resource name from ResourceNames collection in PetDemo.Tests.Common.Resources</param>
        protected void SetupHttpHandler(HttpStatusCode statusCode, string resourceName)
        {
            var testContent = EmbeddedResourceReader.ReadAsJsonString(resourceName);
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
