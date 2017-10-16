using System.Linq;
using PetDemo.Model;
using PetDemo.Model.Enums;
using PetDemo.Web.ModelMappers.Inerfaces;
using PetDemo.Web.Models;

namespace PetDemo.Web.ModelMappers
{
    public class CatViewModelMapper : ICatViewModelMapper
    {
        public CatViewModel Map(Person[] people)
        {
            var noCatsResult = new CatViewModel { NoResult = true };
            if (people == null || !people.Any())
                return noCatsResult;

            var peopleWithCats = people.Where(p => p.Pets != null && p.Pets.Any(x => x.Type == PetType.Cat)).ToList();
            if (!peopleWithCats.Any())
                return noCatsResult;

            var viewModel = new CatViewModel
            {
                MaleOwnedCats = peopleWithCats.Where(x => x.Gender == Gender.Male)
                    .SelectMany(x => x.Pets).Where(x => x.Type == PetType.Cat).Select(x => x.Name).OrderBy(x => x).ToArray(),
                FemaleOwnedCats = peopleWithCats.Where(x => x.Gender == Gender.Female)
                    .SelectMany(x => x.Pets).Where(x => x.Type == PetType.Cat).Select(x => x.Name).OrderBy(x => x).ToArray(),
                NoResult = false
            };
            return viewModel;
        }
    }
}
