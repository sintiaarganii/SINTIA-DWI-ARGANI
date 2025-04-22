using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Models;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using SINTIA_DWI_ARGANI.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SINTIA_DWI_ARGANI.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetAllProducts()
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
        public bool AddProduct(ProductDTO product)
        {
            var data = new Product();
            data.NameProduct = product.NameProduct;
            data.Description = product.Description;
            data.Stock = product.Stock;
            data.Price = product.Price;
            data.StatusProduct = product.StatusProduct;
            data.IdCategori = product.IdCategori;

            _context.Add(data);
            _context.SaveChanges();
            return true;
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
