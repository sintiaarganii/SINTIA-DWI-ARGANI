using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Filters;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DB;

namespace SINTIA_DWI_ARGANI.Controllers
{
    [AuthorizeRole("Cashier")]
    public class CashierHomeController : BaseController
    {
        private readonly ApplicationContext _context;

        public CashierHomeController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var name = HttpContext.Session.GetString("Name");

            ViewBag.UserId = userId;
            ViewBag.Name = name;

            var products = _context.Products.ToList();

            var viewModel = new CashierHome
            {
                Products = products
            };

            return View(viewModel);
        }
    }
}
