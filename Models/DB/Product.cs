using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string NameProduct { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0.")]
        public int Stock { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int IdCategori { get; set; }

        public Category Categori { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData StatusProduct { get; set; }

        public ICollection<Order> Orderings { get; set; }
    }
}