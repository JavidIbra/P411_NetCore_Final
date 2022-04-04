using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.Entity;
using CodeAcademyApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Students
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Students.Include(s => s.Group);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Admin/Students/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        // POST: Admin/Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,GroupId,Image,Img")] Student student)
        {
            if (student.Img != null)
            {
                if (!student.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!student.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

                student.Image = await student.Img.UpLoad(_env.WebRootPath, @"img/studentimg");
            }

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,PhoneNumber,GroupId,Image,Img,Id")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }


            if (student.Img != null)
            {
                if (!student.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!student.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

               
                string filepath = Path.Combine(_env.WebRootPath, @"img\studentimg", student.Image);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                student.Image = await student.Img.UpLoad(_env.WebRootPath, @"img/studentimg");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        // GET: Admin/Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Admin/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
