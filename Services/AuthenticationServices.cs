using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SINTIA_DWI_ARGANI.Helper;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Models.DB;
using iText.Commons.Actions.Contexts;
using Microsoft.CodeAnalysis.Scripting;

namespace SINTIA_DWI_ARGANI.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly ApplicationContext _conteks;
        private readonly string _paper;
        private readonly string _iteration;

        public AuthenticationService(ApplicationContext conteks, IConfiguration configuration)
        {
            _paper = configuration.GetSection("Security:Pepper").Value ?? "";
            _iteration = configuration.GetSection("Security:Iteration").Value ?? "";
            _conteks = conteks;
        }


        public async Task<bool> Register(RegistCashierDTO registerDTO)
        {
            try
            {
                if (await _conteks.Cashiers.AnyAsync(u => u.Username == registerDTO.Username))
                    return false;

                var salt = Hasher.GenerateSalt();

                var cashier = new Cashier
                {
                    Name = registerDTO.Name,
                    Username = registerDTO.Username,
                    Salt = salt,
                    PasswordHash = Hasher.ComputeHash(registerDTO.Password, salt, _paper, Convert.ToInt32(_iteration)),
                    Role = "Cashier", // Default role
                    CreatedAt = DateTime.UtcNow,
                    CashierStatus = GeneralStatus.GeneralStatusData.published
                };

                _conteks.Auth.Add(cashier);
                await _conteks.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error di AuthService.Register: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");

                throw; // Lempar kembali exception untuk ditangani pemanggil
            }
        }

        //public async Task<bool> Register(RegistCashierDTO registerDTO)
        //{
        //    try
        //    {
        //        // Hash password
        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

        //        var newUser = new Auth
        //        {
        //            Username = registerDTO.Username,
        //            Password = hashedPassword,
        //            Name = registerDTO.Name,
        //            Role = "Cashier"
        //        };

        //        _context.Auth.Add(newUser);
        //        await _context.SaveChangesAsync(); // Pastikan ini ada

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public async Task<(bool Success, string Role, int UserId)> Login(CashierAccessDTO loginDTO)
        {
            var user = await _conteks.Auth
                .FirstOrDefaultAsync(u =>
                    u.Username.ToLower() == loginDTO.Username.ToLower() &&
                    u.CashierStatus == GeneralStatus.GeneralStatusData.published);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return (false, null, 0);
            }

            System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {loginDTO.Username}");
            System.Diagnostics.Debug.WriteLine($"Hash tersimpan: {user.PasswordHash}");
            System.Diagnostics.Debug.WriteLine($"Salt: {user.Salt}");
            System.Diagnostics.Debug.WriteLine($"Pepper: {_paper}");
            System.Diagnostics.Debug.WriteLine($"Iteration: {_iteration}");

            Console.WriteLine($"Stored hash: {user.PasswordHash}");
            Console.WriteLine($"Using salt: {user.Salt}, Pepper: {_paper}, iterations: {_iteration}");

            var hashResult = Hasher.ComputeHash(loginDTO.Password, user.Salt, _paper, Convert.ToInt32(_iteration));
            Console.WriteLine($"Computed hash: {hashResult}");

            if (hashResult == user.PasswordHash && loginDTO.Username == user.Username)
            {
                return (true, user.Role, user.Id);
            }
            else
            {
                Console.WriteLine("Hash comparison failed");
                return (false, null, 0);
            }
        }
       
        public List<RegistCashierDTO> GetAllCashier()
        {
            var data = _conteks.Auth.Where(x => x.CashierStatus != GeneralStatusData.deleted).Select(x => new RegistCashierDTO
            {
                Id = x.Id,
                Name = x.Name,
                Username = x.Username,
                Password = "*******",
                ConfirmPassword = "*******",
                CashierStatus = x.CashierStatus

            }).ToList();
            return data;
        }

        public Cashier GetCashierById(int id)
        {
            var data = _conteks.Auth.Where(x => x.Id == id && x.CashierStatus != GeneralStatusData.deleted).FirstOrDefault();
            if (data == null)
            {
                return new Cashier();
            }

            return data;
        }

        public bool EditCashier(RegistCashierDTO registerDTO)
        {
            var data = _conteks.Auth.FirstOrDefault(x => x.Id == registerDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.CreatedAt = DateTime.Now;
            data.CashierStatus = registerDTO.CashierStatus;


            _conteks.Auth.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteCashier(int id)
        {
            var data = _conteks.Auth.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.CashierStatus = GeneralStatusData.deleted;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public List<SelectListItem> Cashier()
        {
            var datas = _conteks.Auth
                .Where(x => x.CashierStatus == GeneralStatusData.published)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();


            return datas;
        }
    }
}