using Microsoft.AspNetCore.Mvc;
using Sol_AutoServiceRegistration.Interfaces;
using Sol_AutoServiceRegistration.Models;
using System.Diagnostics;

namespace Sol_AutoServiceRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemo _demo;
        private readonly IDemo_Attribute_Based Demo_Attribute_Based;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
