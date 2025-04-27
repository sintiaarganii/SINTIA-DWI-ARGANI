using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Filters;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Controllers
{
    [AuthorizeRole("Admin")]
    public class DashboardController : BaseController
    {
        private readonly ApplicationContext _context;
        public DashboardController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("LoginCashier", "Authentication");
            }

            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            var todaysOrders = _context.Orders
                .Where(o => o.OrderDate.Date == today)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            var dashboardData = new DailyDashboardDTO
            {
                TodaysOrderCount = _context.Orders.Count(o => o.OrderDate.Date == today),
                TodaysTotalRevenue = _context.OrderDetails
                         .Where(od => od.Order.OrderDate.Date == today)
                         .Sum(od => od.Quantity * od.UnitPrice),
                TodaysProductsSold = _context.OrderDetails
                         .Where(od => od.Order.OrderDate.Date == today)
                         .Sum(od => od.Quantity),
                TodaysOrders = todaysOrders,
                YesterdaysOrderCount = _context.Orders.Count(o => o.OrderDate.Date == yesterday),
                YesterdaysTotalRevenue = _context.OrderDetails
                         .Where(od => od.Order.OrderDate.Date == yesterday)
                         .Sum(od => od.Quantity * od.UnitPrice),
                YesterdaysProductsSold = _context.OrderDetails
                         .Where(od => od.Order.OrderDate.Date == yesterday)
                         .Sum(od => od.Quantity)
            };

            return View(dashboardData);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

