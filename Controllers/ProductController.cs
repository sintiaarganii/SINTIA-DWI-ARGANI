using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Filters;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProduct _interface;
        private readonly ICategory _categori;
        private readonly IValidator<ProductDTO> _productValidator;
        private readonly ApplicationContext _context;
        private readonly ICategory _categoryService;


        public ProductController(IProduct interfaces, ICategory categori, IValidator<ProductDTO> productValidator, ApplicationContext context, ICategory categoryService)
        {
            _interface = interfaces;
            _categori = categori;
            _productValidator = productValidator;
            _context = context;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var products = (from product in _context.Products
                            join category in _context.Categoris
                            on product.IdCategori equals category.Id
                            select new ProductDTO
                            {
                                Id = product.Id,
                                NameProduct = product.NameProduct,
                                Price = product.Price,
                                Stock = product.Stock,
                                CategoriName = category.CategoriName,
                                StatusProduct = product.StatusProduct,
                                CategoryStatus = category.StatusCategori // Populate the new property
                            }).ToList();

            return View(products);
        }

        public IActionResult IndexView()
        {
            var products = (from product in _context.Products
                            join category in _context.Categoris
                            on product.IdCategori equals category.Id
                            select new ProductDTO
                            {
                                Id = product.Id,
                                NameProduct = product.NameProduct,
                                Price = product.Price,
                                Stock = product.Stock,
                                CategoriName = category.CategoriName,
                                StatusProduct = product.StatusProduct,
                                CategoryStatus = category.StatusCategori // Populate the new property
                            }).ToList();

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
        //[HttpPost]
        //public IActionResult Edit(ProductDTO product)
        //{
        //    // Perform validation
        //    var validationResult = _productValidator.Validate(product);

        //    if (!validationResult.IsValid)
        //    {
        //        foreach (var error in validationResult.Errors)
        //        {
        //            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //        }
        //        ViewBag.Categori = _categori.Categories();
        //        return View(product);
        //    }

        //    // Get the category status
        //    var category = _context.Categoris.Find(product.IdCategori);
        //    if (category == null)
        //    {
        //        ModelState.AddModelError("IdCategori", "Category not found");
        //        ViewBag.Categori = _categori.Categories();
        //        return View(product);
        //    }

        //    // Auto-unpublish if category is unpublished or deleted
        //    if (category.StatusCategori != GeneralStatus.GeneralStatusData.published)
        //    {
        //        product.StatusProduct = GeneralStatus.GeneralStatusData.unpublished;
        //        TempData["WarningMessage"] = "Product automatically unpublished because its category is not published";
        //    }

        //    if (product.Id == 0)
        //    {
        //        // Add new product
        //        var addProduct = _interface.AddProduct(product);
        //        if (addProduct)
        //        {
        //            TempData["SuccessMessage"] = "Product added successfully";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ModelState.AddModelError("", "Failed to add product");
        //    }
        //    else
        //    {
        //        // Edit existing product
        //        var existingProduct = _context.Products
        //            .Include(p => p.Categori)
        //            .FirstOrDefault(p => p.Id == product.Id);

        //        if (existingProduct == null)
        //        {
        //            return NotFound();
        //        }

        //        // Prevent publishing if category is unpublished
        //        if (product.StatusProduct == GeneralStatus.GeneralStatusData.published &&
        //            existingProduct.Categori.StatusCategori != GeneralStatus.GeneralStatusData.published)
        //        {
        //            product.StatusProduct = GeneralStatus.GeneralStatusData.unpublished;
        //            TempData["WarningMessage"] = "Cannot publish product because its category is not published";
        //        }

        //        var editProduct = _interface.EditProduct(product);
        //        if (editProduct)
        //        {
        //            TempData["SuccessMessage"] = "Product updated successfully";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ModelState.AddModelError("", "Failed to update product");
        //    }

        //    ViewBag.Categori = _categori.Categories();
        //    return View(product);
        //}

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