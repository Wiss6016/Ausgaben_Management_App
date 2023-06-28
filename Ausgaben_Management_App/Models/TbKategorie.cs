using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ausgaben_Management_App.Models
{
    public class TbKategorie
    {
        [Key]
        public int kategorieId { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [Required(ErrorMessage ="Titel musste ausgefult")]
        public string Titel { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";
        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Ausgaben";
        [NotMapped]
        public string? TitelMitIcon {
            get
            {
                return this.Icon + "" +this.Titel;
            }
            
            
        }   

    }
}
