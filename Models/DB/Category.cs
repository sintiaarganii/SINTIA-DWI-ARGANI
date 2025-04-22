using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters.")]
        public string CategoriName { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData StatusCategori { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}