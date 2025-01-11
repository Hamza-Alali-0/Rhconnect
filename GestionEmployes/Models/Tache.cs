using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEmployes.Models
{
    [Table("Taches", Schema = "HR")]
    public class Tache
    {
        [Key]
        [Display(Name = "ID")]
        public int TacheId { get; set; }

        [Display(Name = "Nom de la Tâche")]
        [Column(TypeName = "varchar(200)")]
        public string NomTache { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Column(TypeName = "varchar(500)")]
        public string? Description { get; set; }

        [Display(Name = "Date de début")]
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [Display(Name = "Date de fin")]
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        [Display(Name = "Statut")]
        [Column(TypeName = "varchar(20)")]
        public string Statut { get; set; } = string.Empty;

        public int EmployeId { get; set; }
        public Employe? Employe { get; set; }

        [Display(Name = "Priorité")]
        [Column(TypeName = "varchar(20)")]
        public string Priorite { get; set; } = string.Empty;
    }
}