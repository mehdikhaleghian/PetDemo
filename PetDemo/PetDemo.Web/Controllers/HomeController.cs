using System;
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
        private readonly CatViewModelMapper _catMapper;

        public HomeController(IPeopleManager peopleManager, CatViewModelMapper catMapper)
        {
            _peopleManager = peopleManager;
            _catMapper = catMapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Cats()
        {
            try
            {
                var people = await _peopleManager.GetPeopleAsync();
                var viewModel = _catMapper.Map(people);
                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
