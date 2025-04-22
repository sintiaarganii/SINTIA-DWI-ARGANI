using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Cashier
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Salt is required.")]
        public string Salt { get; set; }

        [Required(ErrorMessage = "Password hash is required.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "Role must be either 'Admin' or 'User'.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public GeneralStatusData CashierStatus { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        public DateTime CreatedAt { get; set; }
    }
}