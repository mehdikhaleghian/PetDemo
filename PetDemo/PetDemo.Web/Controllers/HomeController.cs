using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetDemo.Proxy.Interfaces;
using PetDemo.Web.ModelMappers;
using PetDemo.Web.Models;

namespace PetDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleManager _peopleManager;
        private readonly PeopleViewModelMapper _peopleMapper;

        public HomeController(IPeopleManager peopleManager, PeopleViewModelMapper peopleMapper)
        {
            _peopleManager = peopleManager;
            _peopleMapper = peopleMapper;
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
            var viewModel = _peopleMapper.Map(people);
            return View(viewModel);
        }
    }
}
