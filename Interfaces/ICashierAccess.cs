using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface ICashierAccess
    {
        public bool AddCashier(CashierAccessDTO users);
        public List<CashierAccessDTO> GetlistCashier();
        public CashierAccess GetById(int id);
        public bool EditCashier(CashierAccessDTO userAccessDTO);
        public bool DeleteCashier(int id);
        public bool Login(string username, string password);
    }
}
