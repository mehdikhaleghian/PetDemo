using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PetDemo.Tests.Common;

namespace PetDemo.Tests.Isolation.Service.PeopleServiceTests
{
    public class GetPeopleDataOrientedTests : PeopleServiceTestsBase
    {
        [Test]
        public async Task GetPeopleAsync_Should_Not_Return_Null_When_HttpHandler_Returns_OK_Response()
        {
            SetupHttpHandler(HttpStatusCode.OK, Resources.PeopleJson);
            var people = await Sut.GetPeopleAsync();
            people.Should().NotBeNull();
        }

        [Test]
        public async Task GetPeopleAsync_Should_Return_The_Correct_Number_Of_Results_Which_Is_Retrieved_From_API()
        {
            SetupHttpHandler(HttpStatusCode.OK, Resources.PeopleJson);
            var people = await Sut.GetPeopleAsync();
            people.Length.Should().Be(6);
        }

        [Test]
        public async Task GetPeopleAsync_Should_Return_The_Count_Of_Pets_Correctly()
        {
            SetupHttpHandler(HttpStatusCode.OK, Resources.PeopleJson);
            var people = await Sut.GetPeopleAsync();
            people.First(x => x.Name == "Alice").Pets.Count().Should().Be(2);
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }

        [Test]
        public async Task GetPeopleAsync_Should_Return_Null_When_Pets_Is_Set_As_Null()
        {
            SetupHttpHandler(HttpStatusCode.OK, Resources.PeopleJson);
            var people = await Sut.GetPeopleAsync();
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }
    }
}