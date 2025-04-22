using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProduct _interface;
        private readonly ICategory _categori;
        public ProductController(IProduct interfaces, ICategory categori)
        {
            _interface = interfaces;
            _categori = categori;
        }

        public IActionResult Index()
        {
            var products = _interface.GetAllProducts();
            return View(products);
        }
        public IActionResult IndexView()
        {
            var products = _interface.GetAllProducts();
            return View(products);
        }
        public IActionResult Edit(int Id)
        {
            ViewBag.Categori = _categori.Categories();
            var product = _interface.GetProductById(Id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            if (product.Id == 0)
            {
                var addProduct = _interface.AddProduct(product);
                if (addProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var editProduct = _interface.EditProduct(product);
                if (editProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var deleteProduct = _interface.DeletedProduct(Id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this product");
        }

    }
}
