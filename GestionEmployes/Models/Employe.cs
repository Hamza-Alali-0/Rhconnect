using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestionEmployes.Models
{
    [Table("Employes",Schema ="HR")]
    public class Employe
    {
        [Key]
        [Display(Name = "ID")]
        public int EmployeId { get; set; }
        [Display(Name = "Nom")]
        [Column(TypeName = "varchar(200)")]
        public string NomEmploye { get; set; } = String.Empty;
        [Display(Name = "Image user")]
        [Column(TypeName = "varchar(200)")]
        public string? UserImage { get; set; }

        [Display(Name = "Date de naissance ")]
        [DataType(DataType.Date)]

        public DateTime DateNaissance { get; set; }
        [Display(Name = "Salaire ")]
        [Column(TypeName = "decimal(12,2)")]

        public decimal Salaire { get; set; }

        [Display(Name = "Date d'embauche ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}")]
        public DateTime DateEmbauche { get; set; }
        [Required]
        [Display(Name = "National ID")]
        [MaxLength(5)]
        [MinLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string NationalId { get; set; }
        public int DepartementId { get; set; }
        public Departement? Departement { get; set; }
    }
}
