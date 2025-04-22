using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthentication _iauth;
        private readonly ApplicationContext _context;
        public AuthenticationController(IAuthentication auth, ApplicationContext context)
        {
            _iauth = auth;
            _context = context;
        }

        public IActionResult LoginCashier()
        {
            return View();
        }

        public IActionResult RegisterCashier()
        {
            return View();
        }
        public IActionResult Index()
        {
            var data = _iauth.GetAllCashier();
            return View(data);
        }

        public IActionResult EditCashier(int id)
        {
            var data = _iauth.GetCashierById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditCashier(RegistCashierDTO registerDTO)
        {
            var data = _iauth.EditCashier(registerDTO);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _iauth.DeleteCashier(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Gagal menghapus User.");
        }

        [HttpPost]
        public async Task<IActionResult> LoginCashier(CashierAccessDTO cashierAccessDTO) 
        {
            System.Diagnostics.Debug.WriteLine("cek3210");
            var (success, role, userId) = await _iauth.Login(cashierAccessDTO);

            if (success)
            {
                System.Diagnostics.Debug.WriteLine("cek43210");
                var cashier = await _context.Auth.FindAsync(userId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, cashier.Name),
                    new Claim(ClaimTypes.NameIdentifier, cashier.Username),
                    new Claim(ClaimTypes.Role, cashier.Role),
                    new Claim("UserId", cashier.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // 2. Set Session Variables
                HttpContext.Session.SetString("Username", cashier.Username);
                HttpContext.Session.SetString("Name", cashier.Name);
                HttpContext.Session.SetString("Role", cashier.Role);
                HttpContext.Session.SetInt32("UserId", cashier.Id);
                HttpContext.Session.SetString("IsLoggedIn", "true");

                // Redirect berdasarkan peran
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard"); // Halaman Admin
                }
                else if (role == "Cashier")
                {
                    return RedirectToAction("Index", "CashierHome"); // Halaman User
                }
            }

            TempData["ErrorMessage"] = "Username atau Password salah!";
            return View(cashierAccessDTO);
        }

        public async Task<IActionResult> Logout()
        {
            // 1. Clear Authentication Cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Clear Session
            HttpContext.Session.Clear();

            // 3. Optional: Expire Session Cookie
            Response.Cookies.Append("ASP.NET_SessionId", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            return RedirectToAction("LoginCashier");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCashier(RegistCashierDTO registerDTO)
        {
            if (registerDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password minimal 7 karakter";
                return View(registerDTO);
            }

            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Password dan Konfirmasi Password harus sama!";
                return View(registerDTO);
            }

            var existingUser = await _context.Auth
                .FirstOrDefaultAsync(x => x.Username == registerDTO.Username);

            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Username sudah digunakan. Silakan pilih yang lain.";
                return View(registerDTO);
            }

            try
            {
                var result = await _iauth.Register(registerDTO);
                if (result)
                {
                    TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login.";
                    return RedirectToAction("LoginCashier");
                }

                TempData["ErrorMessage"] = "Gagal mendaftarkan user. Silakan coba lagi.";
                return View(registerDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrasi: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                TempData["ErrorMessage"] = $"Terjadi kesalahan saat mendaftarkan cashier: {ex.Message}";
                return View(registerDTO);
            }

        }



    }
}