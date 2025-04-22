using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IAdminAccess
    {
        public bool AddAdmin(AdminAccessDTO Admin);
        public bool Login(string username, string password);
        public AdminAccess GetById(int id);
        public List<AdminAccessDTO> GetlistAdmin();

        public bool EditAdmin(AdminAccessDTO AccessDTO);
    }
}
