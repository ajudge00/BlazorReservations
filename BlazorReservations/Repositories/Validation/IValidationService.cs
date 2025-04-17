using BlazorReservations.Entities;

namespace BlazorReservations.Repositories.Validation
{
    public interface IValidationService
    {
        Task<List<string>> IsSzobaKategoriaValid(SzobaKategoria kategoria);
    }
}
