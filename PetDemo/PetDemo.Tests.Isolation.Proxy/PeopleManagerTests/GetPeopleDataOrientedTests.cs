using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace PetDemo.Tests.Isolation.Proxy.PeopleManagerTests
{
    public class GetPeopleDataOrientedTests : PeopleManagerTestsBase
    {
        [Test]
        public void GetPeople_Should_Not_Return_Null_When_HttpHandler_Returns_OK_Response()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = Sut.GetPeople();
            people.Should().NotBeNull();
        }

        [Test]
        public void GetPeople_Should_Return_The_Correct_Number_Of_Results_Which_Is_Retrieved_From_API()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = Sut.GetPeople();
            people.Length.Should().Be(6);
        }

        [Test]
        public void GetPeople_Should_Return_The_Count_Of_Pets_Correctly()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = Sut.GetPeople();
            people.First(x => x.Name == "Alice").Pets.Count().Should().Be(2);
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }

        [Test]
        public void GetPeople_Should_Return_Null_When_Pets_Is_Set_As_Null()
        {
            SetupHttpHandler(HttpStatusCode.OK);
            var people = Sut.GetPeople();
            people.First(x => x.Name == "Steve").Pets.Should().BeNull();
        }
    }
}