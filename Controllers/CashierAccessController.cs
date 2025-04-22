using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;


namespace SINTIA_DWI_ARGANI.Controllers
{
    public class CashierAccessController : BaseController
    {
        private readonly IAuthentication _authentication;
        private readonly ApplicationContext _context;   
        public CashierAccessController(IAuthentication authentication, ApplicationContext context)
        {
            _context = context;
            _authentication = authentication;
        }

        public IActionResult RegisterCashier()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            var data = _authentication.GetAllCashier();
            return View(data);
        }

        public IActionResult Edit(int id)
        {
            var data = _authentication.GetCashierById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edited(RegistCashierDTO CashierAccessDTO)
        {
            var data = _authentication.EditCashier(CashierAccessDTO);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _authentication.DeleteCashier(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this Cashiers.");
        }

        //[HttpPost]
        //public IActionResult Login(RegistCashierDTO loginDTO)
        //{
        //    try
        //    {
        //        var datauser = _authentication.Login (loginDTO.Username, loginDTO.Password);
        //        if (datauser)
        //        {
        //            return RedirectToAction("Index", "Dashboard");
        //        }

        //        TempData["ErrorMessage"] = "Username or Password is incorrect!";
        //        return View(loginDTO);
        //    }
        //    catch (Exception)
        //    {
        //        TempData["ErrorMessage"] = "An error occurred while logging in.";
        //        return View(loginDTO);
        //    }
        //}

        [HttpPost]
        public IActionResult RegisterCashier(RegistCashierDTO CashierAccessDTO)
        {
            if (CashierAccessDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password min 7 character";
                return View(CashierAccessDTO);
            }

            if (CashierAccessDTO.Password != CashierAccessDTO.Password)
            {
                TempData["ErrorMessage"] = "Password and Confirm Password must be the same!";
                return View(CashierAccessDTO);
            }

            var data = _context.Auth
                .FirstOrDefault(x => x.Username == CashierAccessDTO.Username);
            if (data != null)
            {
                TempData["ErrorMessage"] = "Username is already in use. Please choose another username.";
                return View(CashierAccessDTO);
            }

            try
            {
                var datauser = _authentication.EditCashier(CashierAccessDTO);
                if (datauser)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction("Login", "CashierAccess");
                }

                TempData["ErrorMessage"] = "Failed to register Cashiers. Please try again.";
                return View(CashierAccessDTO);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while registering the Cashiers.";
                return View(CashierAccessDTO);
            }
        }
    }
}
