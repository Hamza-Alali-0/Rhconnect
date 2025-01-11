using GestionEmployes.Data;
using GestionEmployes.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmployes.Controllers
{
    public class DepartementController : Controller
    {
        private readonly GestionDbContext _context;

        public DepartementController(GestionDbContext context)
        {
            _context = context;
        }

        //  Departement/Create
        public IActionResult Create()
        {
            return View();
        }

        //  Departement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement departement)
        {
            if (ModelState.IsValid)
            {
                // Add the new department to the database
                _context.Departements.Add(departement);
                _context.SaveChanges();

                // Redirect to the list of departments 
                return RedirectToAction(nameof(Index)); 
            }

            
            return View(departement);
        }
        //  Departement/Delete
        public IActionResult Delete(int id)
        {
            var departement = _context.Departements.FirstOrDefault(d => d.DepartmentID == id);
            if (departement == null)
            {
                return NotFound();
            }
            return View(departement);
        }

        //  Departement/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var departement = _context.Departements.FirstOrDefault(d => d.DepartmentID == id);
            if (departement != null)
            {
                _context.Departements.Remove(departement);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Index()
        {
            var departments = _context.Departements.ToList();
            return View(departments);
        }
    }
}
