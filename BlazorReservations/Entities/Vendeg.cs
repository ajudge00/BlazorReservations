using System.ComponentModel.DataAnnotations;

namespace BlazorReservations.Entities
{
    public class Vendeg
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Név mező kitöltése kötelező.")]
        public string Nev { get; set; }
        
        [Required(ErrorMessage = "Születési dátum megadása kötelező.")]
        public DateOnly Szuletett { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
