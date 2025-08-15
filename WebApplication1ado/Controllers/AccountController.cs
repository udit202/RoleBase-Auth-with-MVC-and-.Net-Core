using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1ado.Helpers;
using WebApplication1ado.Models;

namespace WebApplication1ado.Controllers
{
    public class AccountController : Controller
    {
         private string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string hashedPassword = PasswordHelper.HashPassword(model.Password);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword); // store hashed password
                    cmd.Parameters.AddWithValue("@Role", model.Role);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["Message"] = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string hashedPassword = PasswordHelper.HashPassword(model.Password);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserByCredentials", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword); // compare hashed password

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["UserId"] = reader["Id"];
                        Session["Username"] = reader["Username"];
                        Session["Role"] = reader["Role"];

                        if (reader["Role"].ToString() == "Admin")
                            return RedirectToAction("Index", "Home");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}