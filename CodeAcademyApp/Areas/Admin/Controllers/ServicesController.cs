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

    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public ServicesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            //return Content("okay");
            var service = _context.Services;
            return View(await service.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services

                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Image,Img,Text")] Service service)
        {
            //return Json(educationCategory);
            if (service.Img != null)
            {
                if (!service.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!service.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

                service.Image = await service.Img.UpLoad(_env.WebRootPath, @"img/serviceimg");
            }


            if (ModelState.IsValid)
            {

                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Image,Id")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (service.Img != null)
            {
                if (!service.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "File Shekil olmalidir");
                    return View();
                }

                if (!service.Img.IsValidImage(700))
                {
                    ModelState.AddModelError("Img", "Fayl max 700 kb ola biler!");
                    return View();
                }

                string filepath = Path.Combine(_env.WebRootPath, @"img\serviceimg", service.Image);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                service.Image = await service.Img.UpLoad(_env.WebRootPath, @"img/serviceimg");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services

                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
