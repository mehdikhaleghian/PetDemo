using FluentAssert;
using NUnit.Framework;
using PetDemo.Model;
using PetDemo.Model.Enums;
using PetDemo.Web.ModelMappers;

namespace PetDemo.Tests.Isolation.Web.ModelMappers
{
    [TestFixture]
    public class PeopleViewModelMapperTests
    {
        private PeopleViewModelMapper _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new PeopleViewModelMapper();
        }

        [Test]
        public void Should_Return_NoResult_If_The_Given_PeopleList_Is_Null()
        {
            var actual = _sut.Map(null);
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeTrue();
        }

        [Test]
        public void Should_Return_NoResult_If_The_Given_PeopleList_Is_Empty()
        {
            var actual = _sut.Map(new Person[0]);
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeTrue();
        }

        [Test]
        public void Should_Not_Return_No_Result_If_There_Are_Items_In_The_Given_PeopleList()
        {
            var actual = _sut.Map(new Person[1] { new Person { Name = "Mehdi", Gender = Gender.Male, Pets = null } });
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeFalse();
            actual.MaleOwnedAnimals.Length.ShouldBeEqualTo(0);
        }
    }
}
