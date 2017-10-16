using System.Linq;
using FluentAssert;
using NUnit.Framework;
using PetDemo.Model;
using PetDemo.Model.Enums;
using PetDemo.Web.ModelMappers;

namespace PetDemo.Tests.Isolation.Web.ModelMappers
{
    [TestFixture]
    public class CatViewModelMapperTests
    {
        private CatViewModelMapper _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CatViewModelMapper();
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
        public void Should_Return_NoResult_If_There_Are_No_Cats_In_The_Given_PeopleList()
        {
            var actual = _sut.Map(new[] {
                new Person
                {
                    Name = "Mehdi", Gender = Gender.Male,
                    Pets = new[]{new Pet { Name = "Max", Type = PetType.Dog}, new Pet { Name = "RedFish",Type = PetType.Fish} }

                }
            });
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeTrue();
        }

        [Test]
        public void Should_Not_Return_No_Result_If_There_Are_Cats_In_The_Given_PeopleList()
        {
            var actual = _sut.Map(new[] {
                new Person {
                    Name = "Mehdi",
                    Gender = Gender.Male,
                    Pets = new[]
                    {
                        new Pet { Name = "Alex", Type = PetType.Cat }
                    }
                }
            });
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeFalse();
            actual.MaleOwnedCats.Length.ShouldBeEqualTo(1);
        }

        [Test]
        public void Should_Return_The_Result_Ordered_By_Pets_Names()
        {
            var actual = _sut.Map(new[] {
                new Person
                {
                    Name = "Mehdi", Gender = Gender.Male,
                    Pets = new[]{new Pet { Name = "Max", Type = PetType.Cat}, new Pet { Name = "James",Type = PetType.Cat} }

                },
                new Person
                {
                    Name = "Mehdi", Gender = Gender.Female,
                    Pets = new[]{new Pet { Name = "Alex",Type = PetType.Cat}, new Pet { Name = "Bravy",Type = PetType.Cat} }
                }
            });
            actual.ShouldNotBeNull();
            actual.NoResult.ShouldBeFalse();
            actual.MaleOwnedCats.Length.ShouldBeEqualTo(2);
            actual.FemaleOwnedCats.Length.ShouldBeEqualTo(2);
            actual.MaleOwnedCats.First().ShouldBeEqualTo("James");
            actual.MaleOwnedCats.Last().ShouldBeEqualTo("Max");
            actual.FemaleOwnedCats.First().ShouldBeEqualTo("Alex");
            actual.FemaleOwnedCats.Last().ShouldBeEqualTo("Bravy");
        }
    }
}
