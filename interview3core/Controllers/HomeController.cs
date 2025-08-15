using interview3core.Attributes;
using interview3core.Models;
using interview3core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace interview3core.Controllers
{
    [CustomAuthorize("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudents _students;

        public HomeController(ILogger<HomeController> logger, IStudents students)
        {
            _logger = logger;
            _students = students;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _students.GetStudentsAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Students student)
        {
            var data = await _students.createStudentasyn(student);
            if (data == false)
            {
                ViewBag.status = true;
                ViewBag.message = "data Edited SuccessFully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.status = true;
                ViewBag.message = "data Edited SuccessFully";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _students.GetStudentByID(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Students student)
        {
            var data = await _students.updateStudentasyn(student);
            if (data == false)
            { 
                ViewBag.status = true;
            ViewBag.message = "data Edited SuccessFully";
            return RedirectToAction("Edit");
        }
            else
            {
                ViewBag.status = true;
                ViewBag.message = "data Edited SuccessFully";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _students.GetStudentByID(id);
            return View(data);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _students.GetStudentByID(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var data = await _students.deleteStudentasyn(id);
            if (data == false)
            {
                ViewBag.status = true;
                ViewBag.message = "data Edited SuccessFully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.status = true;
                ViewBag.message = "data Edited SuccessFully";
                return RedirectToAction("Index");
            }
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
