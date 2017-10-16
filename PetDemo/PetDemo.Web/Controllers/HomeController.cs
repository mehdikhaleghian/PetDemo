using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetDemo.Proxy.Interfaces;
using PetDemo.Web.ModelMappers.Inerfaces;
using PetDemo.Web.Models;

namespace PetDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleManager _peopleManager;
        private readonly ICatViewModelMapper _catMapper;
        private readonly ILogger _logger;

        public HomeController(IPeopleManager peopleManager, ICatViewModelMapper catMapper, ILogger<HomeController> logger)
        {
            _peopleManager = peopleManager;
            _catMapper = catMapper;
            _logger = logger;
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
                _logger.LogInformation("Start reading cats from the hosted json resource");
                var people = await _peopleManager.GetPeopleAsync();
                var viewModel = _catMapper.Map(people);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("An {Exception} happened during Reading Cats", ex);
                return RedirectToAction("Error");
            }
        }
    }
}
