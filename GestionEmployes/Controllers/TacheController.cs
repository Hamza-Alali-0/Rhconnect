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
    public class TacheController : Controller
    {
        private readonly GestionDbContext _context;

        public TacheController(GestionDbContext context)
        {
            _context = context;
        }

        //  Tache/Index
        public async Task<IActionResult> Index()
        {
            var taches = await _context.Taches.Include(t => t.Employe).ToListAsync();
            return View(taches);
        }

        // Tache/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var tache = await _context.Taches
                .Include(t => t.Employe)
                .FirstOrDefaultAsync(m => m.TacheId == id);

            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // Tache/Create
        public IActionResult Create()
        {
            ViewBag.Employes = new SelectList(_context.Employes, "EmployeId", "NomEmploye"); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tache tache)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tache); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
            }

           
            ViewBag.Employes = new SelectList(_context.Employes, "EmployeId", "NomEmploye", tache.EmployeId);
            return View(tache); 
        }

        //  Tache/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
            {
                return NotFound();
            }

          
            ViewBag.Employes = new SelectList(_context.Employes, "EmployeId", "NomEmploye", tache.EmployeId);

            return View(tache);
        }

        //  Tache/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tache tache)
        {
            if (id != tache.TacheId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Employes = new SelectList(_context.Employes, "EmployeId", "Nom", tache.EmployeId);
                return View(tache); 
            }

            try
            {
                _context.Update(tache);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TacheExists(tache.TacheId))
                {
                    return NotFound();
                }
                else
                {
                    // Log the error or show a user-friendly message
                    return View("Error");  // Consider redirecting to a specific error page
                }
            }

            return RedirectToAction(nameof(Index));
        }




        //  Tache/Delete
        public IActionResult Delete(int id)
        {
            var tache = _context.Taches.Include(t => t.Employe)
                                       .FirstOrDefault(t => t.TacheId == id);
            if (tache == null)
            {
                return NotFound();  
            }
            return View(tache);  
        }




        //  Tache/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
            {
                return NotFound();  
            }

            _context.Taches.Remove(tache);  
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));  
        }


       
        public async Task<IActionResult> GetTaskStatistics()
        {
            var totalTaches = await _context.Taches.CountAsync();
            var completedTaches = await _context.Taches.CountAsync(t => t.Statut == "Completed");

            return Json(new { TotalTaches = totalTaches, CompletedTaches = completedTaches });
        }

      
        private bool TacheExists(int id)
        {
            return _context.Taches.Any(t => t.TacheId == id);
        }
    }
}
