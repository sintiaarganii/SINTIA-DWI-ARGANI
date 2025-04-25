using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class RegistCashierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData CashierStatus { get; set; }
    }
}