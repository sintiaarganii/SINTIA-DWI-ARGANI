using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Filters;

namespace SINTIA_DWI_ARGANI.Controllers
{
    [AuthorizeRole("Cashier")]
    public class AboutController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
