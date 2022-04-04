using CodeAcademyApp.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeAcademyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        private readonly AppDbContext db;

        public DashboardController(AppDbContext database)
        {
            this.db = database;
        }
        public IActionResult Index()
        {
            ViewBag.TotalStudents = db.Students.Count();
            ViewBag.TotalGroups = db.Groups.Count();
            ViewBag.TotalTeachers = db.Teachers.Count();
            ViewBag.TotalEducationCategories = db.EducationCategories.Count();

            return View();
        }
    }
}
