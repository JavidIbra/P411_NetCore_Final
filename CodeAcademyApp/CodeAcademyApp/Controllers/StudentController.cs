using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeAcademyApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext db;

        public StudentController(AppDbContext database)
        {
            this.db = database;
        }


        public async Task<IActionResult> SingleStudent(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Student student = await db.Students.Include(x=>x.Group)
                .Include(x => x.Group.Teacher).Include(x => x.Group.EducationCategory).FirstOrDefaultAsync(x=>x.Id==id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
