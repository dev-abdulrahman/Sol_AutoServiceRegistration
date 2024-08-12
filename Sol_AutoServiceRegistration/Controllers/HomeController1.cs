using Microsoft.AspNetCore.Mvc;
using Sol_AutoServiceRegistration.Interfaces;
using Sol_AutoServiceRegistration.Models;
using System.Diagnostics;

namespace Sol_AutoServiceRegistration.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemo _demo;

        public HomeController1(ILogger<HomeController> logger, IDemo demo)
        {
            _logger = logger;
            _demo = demo;
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
