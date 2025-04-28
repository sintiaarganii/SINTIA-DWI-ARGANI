using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Models;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using SINTIA_DWI_ARGANI.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using iText.Commons.Actions.Data;
using System.Diagnostics;

namespace SINTIA_DWI_ARGANI.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public void AutoUpdateProductStatusBasedOnCategory()
        {
            // Ambil semua produk dengan kategori unpublished atau deleted
            var affectedProducts = _context.Products
                .Include(p => p.Categori)
                .Where(p => p.Categori.StatusCategori == GeneralStatusData.unpublished ||
                            p.Categori.StatusCategori == GeneralStatusData.deleted)
                .ToList();

            // Update status produk menjadi unpublished
            foreach (var product in affectedProducts)
            {
                Debug.WriteLine($"Mengubah status produk {product.NameProduct} menjadi unpublished");
                product.StatusProduct = GeneralStatusData.unpublished;
            }

            _context.SaveChanges();
        }
        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(p => p.Categori)
                .Where(p => p.StatusProduct != GeneralStatusData.deleted)
                .ToList();

            foreach (var product in products)
            {
                if (product.Categori.StatusCategori != GeneralStatusData.published)
                {
                    product.StatusProduct = GeneralStatusData.unpublished;
                }
            }
            _context.SaveChanges();

            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                NameProduct = p.NameProduct,
                Price = p.Price,
                Stock = p.Stock,
                CategoriName = p.Categori.CategoriName,
                StatusProduct = p.StatusProduct,
                CategoryStatus = p.Categori.StatusCategori,
                EffectiveStatus = (p.Categori.StatusCategori == GeneralStatusData.published)
                    ? p.StatusProduct
                    : GeneralStatusData.unpublished
            }).ToList();
        }

        public List<SelectListItem> Products()
        {
            var datas = _context.Products.Where(x => x.StatusProduct == GeneralStatusData.published)
                .Select(x => new SelectListItem
                {
                    Text = x.NameProduct,
                    Value = x.Id.ToString()
                }).ToList();
            return datas;
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Categori)
                .Where(x => x.Id == id && x.StatusProduct != GeneralStatusData.deleted)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    NameProduct = p.NameProduct,
                    Price = p.Price,
                    Stock = p.Stock,
                    Description = p.Description,
                    IdCategori = p.IdCategori,
                    CategoriName = p.Categori.CategoriName,
                    StatusProduct = p.StatusProduct
                })
                .FirstOrDefault();

            return product ?? new ProductDTO();
        }
        public bool AddProduct(ProductDTO productDto)
        {
            var category = _context.Categoris.Find(productDto.IdCategori);
            if (category == null)
            {
                Debug.WriteLine($"Kategori dengan ID {productDto.IdCategori} tidak ditemukan.");
                return false;
            }

            // Auto-unpublish if category is unpublished or deleted
            if (category.StatusCategori != GeneralStatusData.published)
            {
                productDto.StatusProduct = GeneralStatusData.unpublished;
            }

            var product = new Product
            {
                NameProduct = productDto.NameProduct,
                Description = productDto.Description,
                Stock = productDto.Stock,
                Price = productDto.Price,
                StatusProduct = productDto.StatusProduct,
                IdCategori = productDto.IdCategori
            };

            try
            {
                _context.Products.Add(product);
                int rowsAffected = _context.SaveChanges();
                Debug.WriteLine($"AddProduct: {rowsAffected} baris ditambahkan.");
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saat menambahkan produk: {ex.Message}");
                return false;
            }
        }

        public bool EditProduct(ProductDTO product)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                Debug.WriteLine($"Produk dengan ID {product.Id} tidak ditemukan.");
                return false;
            }

            data.NameProduct = product.NameProduct;
            data.Stock = product.Stock;
            data.Description = product.Description;
            data.Price = product.Price;
            data.StatusProduct = product.StatusProduct;
            data.IdCategori = product.IdCategori; // Tambahkan ini

            try
            {
                _context.Products.Update(data);
                int rowsAffected = _context.SaveChanges();
                Debug.WriteLine($"EditProduct: {rowsAffected} baris diubah.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saat menyimpan produk: {ex.Message}");
                return false;
            }
        }

        public bool DeletedProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null && product.StatusProduct != GeneralStatusData.deleted)
            {
                product.StatusProduct = GeneralStatusData.deleted;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
