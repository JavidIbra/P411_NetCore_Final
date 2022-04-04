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

    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GroupsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Groups.Include(x =>x.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .Include(x => x.Teacher)
                .Include(x => x.EducationCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            ViewData["EduCategoryId"] = new SelectList(_context.EducationCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TeacherId,EduCategoryId")] Group @group)
        {

            EducationCategory educationCat = await _context.EducationCategories.FirstOrDefaultAsync(x => x.Id == @group.EducationCategoryId);
            @group.Image = educationCat.Image;


            //return Json(@group);

            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", @group.TeacherId);
            ViewData["EduCategoryId"] = new SelectList(_context.Teachers, "Id", "Name", @group.EducationCategoryId);
            return View(@group);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", @group.TeacherId);
            ViewData["EduCategoryId"] = new SelectList(_context.Teachers, "Id", "Name", @group.EducationCategoryId);

            return View(@group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,TeacherId,EduCategoryId,Image,Img,Id")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (@group.Img != null)
            {
                if (!@group.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!@group.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

                string filepath = Path.Combine(_env.WebRootPath, @"img\studentimg", @group.Image);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                @group.Image = await @group.Img.UpLoad(_env.WebRootPath, @"img/groupimg");
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", @group.TeacherId);
            ViewData["EduCategoryId"] = new SelectList(_context.EducationCategories, "Id", "Name", @group.EducationCategoryId);
            return View(@group);
        }

        // GET: Admin/Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //@ => @.Teacher
            var @group = await _context.Groups
                .Include(x => x.Teacher)
                .Include(x => x.EducationCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Admin/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
