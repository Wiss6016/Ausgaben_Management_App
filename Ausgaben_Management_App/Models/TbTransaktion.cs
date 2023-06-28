using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ausgaben_Management_App.Models
{
    public class TbTransaktion
    {
        [Key]
        public int TransaktionId { get; set; }
        //kategorieId
        [Range(1, int.MaxValue,ErrorMessage ="Bitte Kategorie Auswällen")]
        public int kategorieId { get; set; }
        //forgen key
        public TbKategorie? kategorie { get; set; }

        [DisplayFormat(DataFormatString = "{0:n} €")]
        [Range(1, int.MaxValue, ErrorMessage = "Beatrag kann nicht 0 sein")]
        public int Betrag { get; set; }
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        [NotMapped]
        public string? KategorieTitelMitIcon {
            get
            {
                return kategorie == null ? "": kategorie.Icon + " " + kategorie.Titel;
            }
        }
        [NotMapped]
        public string? FormattedBetrag
        {
            get
            {
                //return ((kategorie == null  || kategorie.Type== "Ausgaben") ?"- " :"+ ") + Betrag.ToString("C0");
                return ((kategorie == null || kategorie.Type == "Ausgaben") ? "- " : "+ ") + Betrag.ToString("C0");
            }

        }
    } }
