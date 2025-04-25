using System.ComponentModel.DataAnnotations;
using SINTIA_DWI_ARGANI.Models.DB;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoriName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData StatusCategori { get; set; }
    }
}