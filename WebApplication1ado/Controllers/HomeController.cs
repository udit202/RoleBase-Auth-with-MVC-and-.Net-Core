using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1ado.Models;
using WebApplication1ado.Repositors;

namespace WebApplication1ado.Controllers
{
    [CustomAuthorize("Admin","User")]
    public class HomeController : Controller
    {
        StudentRpo StudentRpo=null;
        public HomeController( ) {
            StudentRpo = new StudentRpo();
        }
        public ActionResult Index()
        {
            var Students = StudentRpo.GetAll().Result;
            return View(Students);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            var insertion = StudentRpo.InsertStudent(student).Result;

            if (insertion)
            {
                TempData["success"] = "Data inserted Successfully";
            }
            else
            {

                TempData["Error"] = "Data inserted Error";
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var student = StudentRpo.GetStudentById(id).Result;
            if(student == null)
            {
                
                TempData["Error"] = "Not any related Data";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            var insertion = StudentRpo.Updatestudent(student).Result;

            if (insertion)
            {
                TempData["success"] = "Data Updated Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Data Updateion Error";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details (int id)
        {
            var student = StudentRpo.GetStudentById(id).Result;
            if (student == null)
            {
                TempData["Error"] = "Not any related Data";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
        public ActionResult Delete(int id)
        {
            var student = StudentRpo.GetStudentById(id).Result;
            if (student == null)
            {

                TempData["Error"] = "Not any related Data";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            var insertion = StudentRpo.DeleteStudent(id).Result;

            if (insertion)
            {
                TempData["success"] = "Data Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Data Deletion Error";
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}