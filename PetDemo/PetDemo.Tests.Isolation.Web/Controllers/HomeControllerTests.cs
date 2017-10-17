using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using PetDemo.Web.Controllers;
using Moq;
using PetDemo.Model;
using PetDemo.Model.Enums;
using PetDemo.Service.Interfaces;
using PetDemo.Web.ModelMappers.Inerfaces;

namespace PetDemo.Tests.Isolation.Web.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IPeopleService> _mockPeopleService;
        private Mock<ILogger<HomeController>> _mockLogger;
        private Mock<ICatViewModelMapper> _mockCatViewModelMapper;
        private HomeController _sut;

        [SetUp]
        public void Setup()
        {
            _mockPeopleService = new Mock<IPeopleService>();
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockCatViewModelMapper = new Mock<ICatViewModelMapper>();
            _sut = new HomeController(_mockPeopleService.Object, _mockCatViewModelMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Cats_Should_Call_PeopleManager_GetPeopleAsync()
        {
            await _sut.Cats();
            _mockPeopleService.Verify(x => x.GetPeopleAsync(), Times.Once);
        }

        [Test]
        public async Task Cats_Should_Call_CatViewModelMapper_With_The_Result_From_GetPeopleAsync()
        {
            var peopleData = Task.FromResult(new[] { new Person { Name = "Mehdi", Age = 36, Gender = Gender.Male } });
            _mockPeopleService.Setup(x => x.GetPeopleAsync())
                .Returns(peopleData);
            await _sut.Cats();
            _mockCatViewModelMapper.Verify(x => x.Map(peopleData.Result), Times.Once);
        }

        [Test]
        public void Cats_Should_Redirect_To_Error_Action_If_GetPeopleAsync_Throws_Exception()
        {
            var exception = new TimeoutException();
            _mockPeopleService.Setup(x => x.GetPeopleAsync())
                .Throws(exception);
            var actual = _sut.Cats().Result;
            actual.Should().BeOfType<RedirectToActionResult>();
            ((RedirectToActionResult)actual).ActionName.ShouldBeEquivalentTo("Error");
        }
    }
}
