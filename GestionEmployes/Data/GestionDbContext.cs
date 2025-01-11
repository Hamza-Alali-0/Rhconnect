using GestionEmployes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmployes.Data
{
    public class GestionDbContext : DbContext
    {
        public GestionDbContext(DbContextOptions<GestionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

        }

       public DbSet<Tache> Taches { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Employe> Employes { get; set; }
       

    }
}
