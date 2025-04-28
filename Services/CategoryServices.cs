using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Services;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace UITraining.Services
{
    public class CategoryServices : ICategory
    {
        private readonly ApplicationContext _context;
        private readonly GeneralStatusData newStatus;

        public CategoryServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<CategoryDTO> GetlistCategori()
        {
            var data = _context.Categoris.Where(x => x.StatusCategori != GeneralStatusData.deleted).Select(x => new CategoryDTO
            {
                Id = x.Id,
                CategoriName = x.CategoriName,
                StatusCategori = x.StatusCategori,

            }).ToList();
            return data;

        }
        public List<SelectListItem> Categories()
        {
            var datas = _context.Categoris.Where(x => x.StatusCategori == GeneralStatusData.published)
                .Select(x => new SelectListItem
                {
                    Text = x.CategoriName,
                    Value = x.Id.ToString()
                }).ToList();
            return datas;
        }

        public Category GetCategoriById(int id)
        {
            var supplier = _context.Categoris.Where(x => x.Id == id && x.StatusCategori != GeneralStatusData.deleted).FirstOrDefault();

            if (supplier == null)
            {
                return new Category();
            }

            return supplier;
        }

        public bool AddCategori(CategoryDTO CategiriDTO)
        {
            var categori = new Category
            {
                CategoriName = CategiriDTO.CategoriName,
                StatusCategori = CategiriDTO.StatusCategori
            };

            _context.Categoris.Add(categori);
            _context.SaveChanges();
            return true;
        }

        public bool EditCategori(CategoryDTO CategiriDTO)
        {
            var data = _context.Categoris.FirstOrDefault(x => x.Id == CategiriDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.CategoriName = CategiriDTO.CategoriName;
            data.StatusCategori = CategiriDTO.StatusCategori;

            _context.Categoris.Update(data);
            _context.SaveChanges();

            // Panggil AutoUpdateProductStatusBasedOnCategory jika status kategori berubah menjadi unpublished atau deleted
            if (data.StatusCategori != GeneralStatusData.published)
            {
                var productService = new ProductServices(_context);
                productService.AutoUpdateProductStatusBasedOnCategory();
            }

            return true;
        }
        public bool DeleteCategori(int Id)
        {
            var category = _context.Categoris.FirstOrDefault(x => x.Id == Id);
            if (category != null && category.StatusCategori != GeneralStatusData.deleted)
            {
                category.StatusCategori = GeneralStatusData.deleted;
                _context.SaveChanges();

                // Panggil AutoUpdateProductStatusBasedOnCategory setelah menghapus kategori
                var productService = new ProductServices(_context);
                productService.AutoUpdateProductStatusBasedOnCategory();

                return true;
            }
            return false;
        }
    }
}
