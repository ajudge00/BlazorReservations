using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorReservations.Entities
{
    public class Foglalas
    {
        public int Id { get; set; }

        [Required]
        public DateTime Kezdete { get; set; } = DateTime.Now;

        [Required]
        public int EjszakakSzama { get; set; }

        [ForeignKey("VendegRef")]
        [Required]
        public int VendegId { get; set; }

        [Display(Name = "Vendég")]
        public virtual Vendeg? VendegRef { get; set; }

        [ForeignKey("SzobaRef")]
        [Required]
        public int SzobaId { get; set; }

        [Display(Name = "Szoba")]
        public virtual Szoba? SzobaRef { get; set; }

        [Required]
        public int SzemelyekSzama { get; set; }

        public string Megjegyzes { get; set; }
    }
}
