using System.Linq;
using FluentAssertions;
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
            actual.Should().NotBeNull();
            actual.NoResult.Should().BeTrue();
        }

        [Test]
        public void Should_Return_NoResult_If_The_Given_PeopleList_Is_Empty()
        {
            var actual = _sut.Map(new Person[0]);
            actual.Should().NotBeNull();
            actual.NoResult.Should().BeTrue();
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
            actual.Should().NotBeNull();
            actual.NoResult.Should().BeTrue();
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
            actual.Should().NotBeNull();
            actual.NoResult.Should().BeFalse();
            actual.MaleOwnedCats.Length.Should().Be(1);
        }

        [Test]
        public void Should_Return_The_Correct_Number_Of_Cats_For_MaleOwner_Groups()
        {
            var testData = GetTestData();
            var actual = _sut.Map(testData);
            actual.MaleOwnedCats.Length.ShouldBeEquivalentTo(
                testData.Where(x => x.Gender == Gender.Male).Sum(x => x.Pets.Count(pet => pet.Type == PetType.Cat)));
        }

        [Test]
        public void Should_Return_The_Correct_Number_Of_Cats_For_FemaleOwner_Groups()
        {
            var testData = GetTestData();
            var actual = _sut.Map(testData);
            actual.FemaleOwnedCats.Length.ShouldBeEquivalentTo(
                testData.Where(x => x.Gender == Gender.Female).Sum(x => x.Pets.Count(pet => pet.Type == PetType.Cat)));
        }

        [Test]
        public void Should_Return_The_Result_Ordered_By_Pets_Names()
        {
            var testData = GetTestData();
            var actual = _sut.Map(testData);
            actual.MaleOwnedCats.First().Should().Be("Max");
            actual.MaleOwnedCats.Last().Should().Be("Zigi");
            actual.FemaleOwnedCats.First().Should().Be("Alex");
            actual.FemaleOwnedCats.Last().Should().Be("Bravy");
        }

        private Person[] GetTestData()
        {
            return new[]
            {
                new Person
                {
                    Name = "Mehdi",
                    Gender = Gender.Male,
                    Pets = new[]
                    {
                        new Pet {Name = "Max", Type = PetType.Cat},
                        new Pet {Name = "Zigi", Type = PetType.Cat},
                        new Pet {Name = "James", Type = PetType.Dog}
                    }

                },
                new Person
                {
                    Name = "Mehdi",
                    Gender = Gender.Female,
                    Pets = new[]
                    {
                        new Pet {Name = "Alex", Type = PetType.Cat},
                        new Pet {Name = "Bravy", Type = PetType.Cat}
                    }
                }
            };
        }
    }
}
