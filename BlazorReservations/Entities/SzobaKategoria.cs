using System.ComponentModel.DataAnnotations;

namespace BlazorReservations.Entities
{
    public class SzobaKategoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Megnevezés megadása kötelező.")]
        //[UniqueKategoriaName(ErrorMessage = "Ez a kategória név már létezik. (custom)")]
        public string Megnevezes { get; set; }

        [Required(ErrorMessage = "Egységár megadása kötelező.")]
        [Range(10, 10000000, ErrorMessage = "Érvénytelen egységár. (custom)")]
        public int EgysegAr {  get; set; }
    }
}
