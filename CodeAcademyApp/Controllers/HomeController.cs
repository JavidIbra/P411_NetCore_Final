using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.Entity;
using CodeAcademyApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeAcademyApp.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext db;

        public HomeController(AppDbContext database)
        {
            this.db = database;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel hvm = new HomeViewModel();
            hvm.contact = await db.Contacts.Include(x=>x.EducationCategory).FirstOrDefaultAsync();
            hvm.educationCategories = await db.EducationCategories.ToListAsync();
            hvm.services = await db.Services.ToListAsync();
            hvm.banners = await db.Banners.ToListAsync();

            return View(hvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact( Contact contact)
        {
            return Json(contact);
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

            await db.Contacts.AddAsync(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
