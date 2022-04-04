using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.Entity;
using CodeAcademyApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeAcademyApp.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;


        public TeacherController(AppDbContext database, UserManager<AppUser> userManager)
        {
            _db = database;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            
            TeacherViewModel tvm = new TeacherViewModel();
            tvm.Teachers= await _db.Teachers.ToListAsync();
            tvm.Groups = await _db.Groups.Include(x=>x.Teacher).ToListAsync();
            tvm.EducationCategories = await _db.EducationCategories.ToListAsync();
            //tvm.User = await _userManager.FindByNameAsync(name);
            string name1 = TempData["name"].ToString();
            tvm.Teacher = await _db.Teachers.FirstOrDefaultAsync(x=>x.Name==name1);

            return View(tvm);
        }
    }
}
