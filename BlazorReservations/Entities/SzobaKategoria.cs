using System.ComponentModel.DataAnnotations;

namespace BlazorReservations.Entities
{
    public class SzobaKategoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Megnevezés megadása kötelező.")]
        public string Megnevezes { get; set; }

        [Required(ErrorMessage = "Egységár megadása kötelező.")]
        public int EgysegAr {  get; set; }
    }
}
