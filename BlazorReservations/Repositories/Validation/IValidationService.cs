using BlazorReservations.Entities;

namespace BlazorReservations.Repositories.Validation
{
    public interface IValidationService
    {
        Task<List<string>> IsFoglalasValid(Foglalas foglalas);
        Task<List<string>> IsSzobaValid(Szoba szoba);
        Task<List<string>> IsSzobaKategoriaValid(SzobaKategoria kategoria);
        List<string> IsVendegValid(Vendeg vendeg);
    }
}
