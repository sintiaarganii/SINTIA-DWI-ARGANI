using Microsoft.AspNetCore.Mvc;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class ProfileController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
