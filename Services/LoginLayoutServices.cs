using SINTIA_DWI_ARGANI.Interfaces;

namespace SINTIA_DWI_ARGANI.Services
{
    public class LoginLayoutServices : ILoginLayout
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginLayoutServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Method untuk mendapatkan layout berdasarkan peran pengguna
        public string GetLayout()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.IsInRole("Admin"))
            {
                return "~/Views/Shared/_Layout.cshtml"; // Layout untuk Admin
            }
            else if (user.IsInRole("Cashier"))
            {
                return "~/Views/Shared/_LayoutCashier.cshtml"; // Layout untuk User
            }

            return "~/Views/Shared/_Layout.cshtml"; // Default layout jika tidak ada peran
        }
    }
}
