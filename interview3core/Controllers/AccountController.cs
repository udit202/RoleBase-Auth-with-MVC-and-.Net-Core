using interview3core.DBconnect;
using interview3core.Helper;
using interview3core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace interview3core.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBconnectionInterview _context;

        public AccountController(DBconnectionInterview context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (!ModelState.IsValid) return View(model);

            var hashedPassword = PasswordHelper.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Password = hashedPassword,
                Role = model.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Message"] = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }

        // GET: Login
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var hashedPassword = PasswordHelper.HashPassword(model.Password);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
