using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models.DTO;


namespace UITraining.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategory _categori;
        public CategoryController(ICategory categori)
        {
            _categori = categori;
        }

        public IActionResult Index()
        {
            var categori = _categori.GetlistCategori();
            return View(categori);
        }
        public IActionResult IndexView()
        {
            var categori = _categori.GetlistCategori();
            return View(categori);
        }
        public IActionResult Edit(int id)
        {
            var categori = _categori.GetCategoriById(id);
            return View(categori);
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO categori)
        {
            if (categori.Id == 0)
            {
                var addSupplier = _categori.AddCategori(categori);
                if (addSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var editSupplier = _categori.EditCategori(categori);
                if (editSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public IActionResult DeleteCategori(int Id)
        {
            var deleteSupplier = _categori.DeleteCategori(Id);
            if (deleteSupplier)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this Categori");
        }
    }
}
