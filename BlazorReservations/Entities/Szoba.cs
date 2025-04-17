using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorReservations.Entities
{
    public class Szoba
    {
        public int Id { get; set; }

        [Required]
        public int SzobaSzam { get; set; }

        [Required]
        public int AgyakSzama { get; set; }

        [ForeignKey("KategoriaRef")]
        [Required]
        public int KategoriaId { get; set; }

        [Display(Name = "Kategória")]
        public virtual SzobaKategoria? KategoriaRef { get; set; }
    }
}
