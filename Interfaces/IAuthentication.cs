using Microsoft.AspNetCore.Mvc.Rendering;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Interfaces
{
    public interface IAuthentication
    {
        public Task<bool> Register(RegistCashierDTO registerDTO);
        public Task<(bool Success, string Role, int UserId)> Login(CashierAccessDTO loginDTO);
        public List<RegistCashierDTO> GetAllCashier();
        public Cashier GetCashierById(int id);
        public bool EditCashier(RegistCashierDTO registerDTO);
        public bool DeleteCashier(int id);
        public List<SelectListItem> Cashier();
    }
}