using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Filters;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProduct _interface;
        private readonly ICategory _categori;
        private readonly IValidator<ProductDTO> _productValidator;

        public ProductController(IProduct interfaces, ICategory categori, IValidator<ProductDTO> productValidator)
        {
            _interface = interfaces;
            _categori = categori;
            _productValidator = productValidator;
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

            if (Id == 0)
            {
                // For add new product
                return View(new ProductDTO());
            }

            var product = _interface.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }

            // Map Product to ProductDTO
            var productDto = new ProductDTO
            {
                Id = product.Id,
                NameProduct = product.NameProduct,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                StatusProduct = product.StatusProduct,
                IdCategori = product.IdCategori
            };

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            // Perform validation
            var validationResult = _productValidator.Validate(product);

            if (!validationResult.IsValid)
            {
                // Add validation errors to ModelState
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                ViewBag.Categori = _categori.Categories();
                return View(product);
            }

            if (product.Id == 0)
            {
                // Add new product
                var addProduct = _interface.AddProduct(product);
                if (addProduct)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to add product");
            }
            else
            {
                // Edit existing product
                var editProduct = _interface.EditProduct(product);
                if (editProduct)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update product");
            }

            ViewBag.Categori = _categori.Categories();
            return View(product);
        }

        public IActionResult Delete(int Id)
        {
            var deleteProduct = _interface.DeletedProduct(Id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot delete this product");
        }
    }
}