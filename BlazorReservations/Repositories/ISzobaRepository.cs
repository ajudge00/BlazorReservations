using BlazorReservations.Entities;

namespace BlazorReservations.Repositories
{
    public interface ISzobaRepository
    {
        Task CreateAsync(Szoba szoba);
        Task<List<Szoba>> ReadAllAsync();
        Task<Szoba> ReadByIdAsync(int id);
        Task UpdateAsync(Szoba szoba);
        Task DeleteAsync(int id);

        Task CreateKategoriaAsync(SzobaKategoria kategoria);
        Task<List<SzobaKategoria>> ReadKategoriaAllAsync();
        Task<SzobaKategoria> ReadKategoriaByIdAsync(int id);
        Task UpdateKategoriaAsync(SzobaKategoria kategoria);
        Task DeleteKategoriaAsync(int id);
        Task<List<SzobaKategoria>> ReadKategoriaFiltered(SzobaKategoriaFilter filter);
        Task<List<Szoba>> ReadFiltered(SzobaFilter filter);
    }
}