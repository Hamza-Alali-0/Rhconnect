using GestionEmployes.Data;
using GestionEmployes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GestionEmployes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestionDbContext _context;

        
        public HomeController(ILogger<HomeController> logger, GestionDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            
            var totalEmployees = await _context.Employes.CountAsync();

            
            var averageSalary = totalEmployees > 0
                ? await _context.Employes.AverageAsync(e => e.Salaire)
                : 0; 

           
            var totalDepartments = await _context.Departements.CountAsync();

            
            var totalTasks = await _context.Taches.CountAsync(); 
            
            var homeViewModel = new HomeViewModel
            {
                TotalEmployees = totalEmployees,
                AverageSalary = averageSalary,
                TotalDepartments = totalDepartments,
                TotalTasks = totalTasks 
            };

            return View(homeViewModel); 
        }
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
