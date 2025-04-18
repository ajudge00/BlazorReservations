using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorReservations.Repositories
{
    public interface IVendegRepository
    {
        Task CreateAsync(Vendeg vendeg);
        Task<List<Vendeg>> ReadAllAsync();
        Task<Vendeg> ReadByIdAsync(int id);
        Task<List<Vendeg>> ReadFiltered(VendegFilter filter);
        Task UpdateAsync(Vendeg vendeg);
        Task DeleteAsync(int id);
    }
}
