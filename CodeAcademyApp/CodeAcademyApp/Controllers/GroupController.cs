using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Controllers
{
    public class GroupController : Controller
    {

        private readonly AppDbContext db;

        public GroupController(AppDbContext database)
        {
            this.db = database;
        }
        public async Task<IActionResult> Index()
        {

            GroupViewModel gvm = new GroupViewModel();
            gvm.Groups =await  db.Groups.Include(x=>x.Teacher).Include(x => x.Students).ToListAsync();
            gvm.EducationCategories =await  db.EducationCategories.ToListAsync();

         
            return View(gvm);
        }

        public async Task<IActionResult> Details(int? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var group = await db.Groups.Include(x=>x.Teacher).Include(x => x.Students).Include(x => x.EducationCategory)

                .FirstOrDefaultAsync(m => m.Id == id);

            if (group.Students.Count()==0)
            {
                return Content("Group-a telebe elave olunmayib");
            }

            if (group == null)
            {
                return NotFound();
            }
        
            return View(group);
        }
    }
}
