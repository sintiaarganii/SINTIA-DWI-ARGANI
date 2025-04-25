using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

        public int IdCategori { get; set; }
        public string CategoriName { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData StatusProduct { get; set; }
    }
}