using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Filters;
using SINTIA_DWI_ARGANI.Models;

namespace SINTIA_DWI_ARGANI.Controllers
{
    [AuthorizeRole("Admin")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
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
