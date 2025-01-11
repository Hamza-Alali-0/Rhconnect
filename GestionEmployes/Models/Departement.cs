using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmployes.Models
{
    [Table("Departements",Schema ="HR")]
    public class Departement
    {
        [Key]
        [Display(Name ="ID")]
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name ="Nom Departement")]
        [Column(TypeName ="varchar(200)")]
        public string NomDepartement {  get; set; }=String.Empty;
    }
}
