using Microsoft.AspNetCore.Mvc.Rendering;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface ICategory
    {
        List<CategoryDTO> GetlistCategori();
        List<SelectListItem> Categories();
        Category GetCategoriById(int id);
        bool AddCategori(CategoryDTO CategiriDTO);
        bool EditCategori(CategoryDTO CategiriDTO);
        bool DeleteCategori(int Id);
    }
}
