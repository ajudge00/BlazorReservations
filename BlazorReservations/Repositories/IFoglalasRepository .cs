using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorReservations.Repositories
{
    public interface IFoglalasRepository
    {
        Task CreateAsync(Foglalas foglalas);
        Task<List<Foglalas>> ReadAllAsync();
        Task<Foglalas> ReadByIdAsync(int id);
        Task UpdateAsync(Foglalas foglalas);
        Task DeleteAsync(int id);
    }
}
