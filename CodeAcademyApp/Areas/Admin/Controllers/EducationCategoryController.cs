using CodeAcademyApp.DAL;
using CodeAcademyApp.Models.Entity;
using CodeAcademyApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class EducationCategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public EducationCategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/EducationCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EducationCategories;
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationCategory = await _context.EducationCategories

                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationCategory == null)
            {
                return NotFound();
            }

            return View(educationCategory);
        }

        // GET: Admin/EducationCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/EducationCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Image,Img")] EducationCategory educationCategory)
        {
            //return Json(educationCategory);
            if (educationCategory.Img != null)
            {
                if (!educationCategory.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!educationCategory.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

                educationCategory.Image = await educationCategory.Img.UpLoad(_env.WebRootPath, @"img/educatimg");
            }
       

            if (ModelState.IsValid)
            {

                _context.Add(educationCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationCategory);
        }

        // GET: Admin/EducationCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var educationCategory = await _context.EducationCategories.FindAsync(id);

            //EducationCategory educationCategory =await _context.EducationCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (educationCategory == null)
            {
                return NotFound();
            }
            return View(educationCategory);
        }

        // POST: Admin/EducationCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Image,Id")] EducationCategory educationCategory)
        {
            if (id != educationCategory.Id)
            {
                return NotFound();
            }


            if (educationCategory.Img != null)
            {
                if (!educationCategory.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!educationCategory.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }
                string filepath = Path.Combine(_env.WebRootPath, @"img\educatimg", educationCategory.Image);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                educationCategory.Image = await educationCategory.Img.UpLoad(_env.WebRootPath, @"img/educatimg");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationCategoryExists(educationCategory.Id))
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
            return View(educationCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationCategory = await _context.EducationCategories

                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationCategory == null)
            {
                return NotFound();
            }

            return View(educationCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationCategory = await _context.EducationCategories.FindAsync(id);
            _context.EducationCategories.Remove(educationCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationCategoryExists(int id)
        {
            return _context.EducationCategories.Any(e => e.Id == id);
        }
    }
}
