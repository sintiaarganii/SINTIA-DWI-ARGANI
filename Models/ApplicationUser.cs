using Microsoft.AspNetCore.Identity;

namespace SINTIA_DWI_ARGANI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}