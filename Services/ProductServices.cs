using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Models;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using SINTIA_DWI_ARGANI.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using iText.Commons.Actions.Data;

namespace SINTIA_DWI_ARGANI.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        /*public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(y => y.Categori)
                .Where(x => x.StatusProduct != GeneralStatusData.deleted)
                .Select(x => new ProductDTO
                {
                    Id = x.Id,
                    NameProduct = x.NameProduct,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    StatusProduct = x.StatusProduct,
                    CategoriName = x.Categori.CategoriName,
                }).ToList();
            return products;
        }*/
        public List<ProductDTO> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Categori)
                .Where(p => p.StatusProduct != GeneralStatusData.deleted)
                .Select(p => new ProductDTO
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
                })
                .ToList();
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

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id && x.StatusProduct != GeneralStatusData.deleted).FirstOrDefault();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }
         public bool AddProduct(ProductDTO productDto)
    {
        var category = _context.Categoris.Find(productDto.IdCategori);

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

        _context.Products.Add(product);
        return _context.SaveChanges() > 0;
    }

        public bool EditProduct(ProductDTO product)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }

            data.NameProduct = product.NameProduct;
            data.Stock = product.Stock;
            data.Description = product.Description;
            data.Price = product.Price;
            data.StatusProduct = product.StatusProduct;

            _context.Products.Update(data);
            _context.SaveChanges();
            return true;
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
