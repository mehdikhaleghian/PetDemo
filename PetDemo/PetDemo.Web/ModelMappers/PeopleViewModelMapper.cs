using System.Linq;
using PetDemo.Model;
using PetDemo.Model.Enums;
using PetDemo.Web.Models;

namespace PetDemo.Web.ModelMappers
{
    public class PeopleViewModelMapper
    {
        public PeopleViewModel Map(Person[] people)
        {
            if (people == null || !people.Any())
                return new PeopleViewModel { NoResult = true };
            var viewModel = new PeopleViewModel
            {
                MaleOwnedAnimals = people.Where(x => x.Gender == Gender.Male && x.Pets != null)
                    .SelectMany(x => x.Pets).Select(x => x.Name).OrderBy(x => x).ToArray(),
                FemaleOwnedAnimals = people.Where(x => x.Gender == Gender.Female && x.Pets != null)
                    .SelectMany(x => x.Pets).Select(x => x.Name).OrderBy(x => x).ToArray(),
                NoResult = false
            };
            return viewModel;
        }
    }
}
