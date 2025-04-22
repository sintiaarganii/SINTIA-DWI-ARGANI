using Microsoft.AspNetCore.Mvc.Rendering;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IProduct
    {
        public List<ProductDTO> GetAllProducts();
        public Product GetProductById(int id);
        public bool AddProduct(ProductDTO product);
        public bool EditProduct(ProductDTO product);
        public bool DeletedProduct(int productId);
        public List<SelectListItem> Products();

    }
}
