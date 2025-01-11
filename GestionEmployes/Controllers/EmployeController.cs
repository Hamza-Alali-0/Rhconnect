using GestionEmployes.Data;
using GestionEmployes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEmployes.Controllers
{
    public class EmployeController : Controller
    {
        private readonly GestionDbContext _context;

        public EmployeController(GestionDbContext context)
        {
            _context = context;
        }

        // GET: Employe/Index
        public async Task<IActionResult> Index()
        {
            var employes = await _context.Employes.Include(e => e.Departement).ToListAsync();
            return View(employes);
        }

        //  Employe/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Departement)
                .FirstOrDefaultAsync(m => m.EmployeId == id);

            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        //  Employe/Create
        public IActionResult Create()
        {
            
            ViewBag.Departements = new SelectList(_context.Departements, "DepartmentID", "NomDepartement");
            return View();
        }

        //  Employe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employe employe, IFormFile userImage)
        {
            if (ModelState.IsValid)
            {
                if (userImage != null && userImage.Length > 0)
                {
                   
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", userImage.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userImage.CopyToAsync(stream);
                    }

                    employe.UserImage = userImage.FileName;
                }

                _context.Add(employe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.Departements = new SelectList(_context.Departements, "DepartmentID", "NomDepartement", employe.DepartementId);
            return View(employe);
        }

        //  Employe/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            
            ViewBag.Departements = new SelectList(_context.Departements, "DepartmentID", "NomDepartement", employe.DepartementId);
            return View(employe);
        }

        //  Employe/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employe employe, IFormFile userImage)
        {
            if (id != employe.EmployeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userImage != null && userImage.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", userImage.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await userImage.CopyToAsync(stream);
                        }

                        employe.UserImage = userImage.FileName;
                    }

                    _context.Update(employe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeExists(employe.EmployeId))
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

            ViewBag.Departements = new SelectList(_context.Departements, "DepartmentID", "NomDepartement", employe.DepartementId);
            return View(employe);
        }

        //  Employe/Delete/
        public IActionResult Delete(int id)
        {
            var employe = _context.Employes.Include(e => e.Departement)
                                           .FirstOrDefault(e => e.EmployeId == id);
            if (employe == null)
            {
                return NotFound();  
            }
            return View(employe);  
        }

        //  Employe/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound(); 
            }

            try
            {
               
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", employe.UserImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);  
                }

                _context.Employes.Remove(employe);  
                await _context.SaveChangesAsync();  
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));  
        }

        
        public async Task<IActionResult> GetEmployeeStatistics()
        {
            var totalEmployees = await _context.Employes.CountAsync();
            var averageSalary = await _context.Employes.AverageAsync(e => e.Salaire);

            return Json(new { TotalEmployees = totalEmployees, AverageSalary = averageSalary });
        }

        
        private bool EmployeExists(int id)
        {
            return _context.Employes.Any(e => e.EmployeId == id);
        }
    }
}
