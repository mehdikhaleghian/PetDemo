using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetDemo.Model.Enums;
using PetDemo.Proxy.Interfaces;
using PetDemo.Web.Models;

namespace PetDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleManager _peopleManager;

        public HomeController(IPeopleManager peopleManager)
        {
            _peopleManager = peopleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> People()
        {
            var people = await _peopleManager.GetPeopleAsync();
            var viewModel = new PeopleViewModel();
            viewModel.MaleOwnedAnimals = people.Where(x => x.Gender == Gender.Male && x.Pets != null)
                .SelectMany(x => x.Pets).Select(x => x.Name).OrderBy(x => x).ToArray();
            viewModel.FemaleOwnedAnimals = people.Where(x => x.Gender == Gender.Female && x.Pets != null)
                .SelectMany(x => x.Pets).Select(x => x.Name).OrderBy(x => x).ToArray();
            return View(viewModel);
        }
    }
}
