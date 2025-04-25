using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models.DTO;
using FluentValidation;
using System.Diagnostics;
using SINTIA_DWI_ARGANI.Filters;

namespace UITraining.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategory _categori;
        private readonly IValidator<CategoryDTO> _categoryValidator;

        public CategoryController(ICategory categori, IValidator<CategoryDTO> categoryValidator)
        {
            _categori = categori;
            _categoryValidator = categoryValidator;
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
            var data = _categori.GetCategoriById(id);
            var categori = id == 0
                ? new CategoryDTO()
                : new CategoryDTO
                {
                    Id = data.Id,
                    CategoriName = data.CategoriName
                };

            if (categori == null)
            {
                return NotFound();
            }
            return View(categori);
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO categori)
        {
            Debug.WriteLine($"=== Mulai Edit (POST) ===");
            Debug.WriteLine($"Id: {categori.Id}, CategoryName: {categori.CategoriName}, Status: {categori.StatusCategori}");

            var validationResult = _categoryValidator.Validate(categori);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Debug.WriteLine($"Validation error: {error.PropertyName} - {error.ErrorMessage}");
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState tidak valid. Berikut adalah error:");
                foreach (var error in ModelState)
                {
                    foreach (var err in error.Value.Errors)
                    {
                        Debug.WriteLine($"Error pada {error.Key}: {err.ErrorMessage}");
                    }
                }
                return View(categori);
            }

            try
            {
                var existingCategory = _categori.GetlistCategori()
                    .FirstOrDefault(c => c.CategoriName == categori.CategoriName && c.Id != categori.Id);
                if (existingCategory != null)
                {
                    ModelState.AddModelError("CategoriName", "Category name already exists.");
                    return View(categori);
                }

                bool success;
                if (categori.Id == 0)
                {
                    Debug.WriteLine("Menambahkan kategori baru...");
                    success = _categori.AddCategori(categori);
                    Debug.WriteLine($"AddCategori result: {success}");
                }
                else
                {
                    Debug.WriteLine("Mengedit kategori...");
                    success = _categori.EditCategori(categori);
                    Debug.WriteLine($"EditCategori result: {success}");
                }

                if (success)
                {
                    Debug.WriteLine("Operasi berhasil, mengalihkan ke Index...");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Debug.WriteLine("Gagal menyimpan kategori.");
                    ModelState.AddModelError("", "Gagal menyimpan kategori. Silakan coba lagi.");
                    return View(categori);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terjadi kesalahan: {ex.Message}");
                Debug.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"Terjadi kesalahan saat menyimpan kategori: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
                return View(categori);
            }
        }

        public IActionResult DeleteCategori(int Id)
        {
            try
            {
                Debug.WriteLine($"Menghapus kategori dengan Id: {Id}");
                var deleteSuccess = _categori.DeleteCategori(Id);
                if (deleteSuccess)
                {
                    Debug.WriteLine("Kategori berhasil dihapus.");
                    return RedirectToAction(nameof(Index));
                }
                Debug.WriteLine("Gagal menghapus kategori.");
                return BadRequest("Cannot delete this category.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terjadi kesalahan saat menghapus: {ex.Message}");
                return BadRequest($"Error deleting category: {ex.Message}");
            }
        }
    }
}