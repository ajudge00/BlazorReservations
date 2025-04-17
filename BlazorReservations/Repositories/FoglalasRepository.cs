using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorReservations.Repositories
{
    public class FoglalasRepository : IFoglalasRepository
    {
        private readonly BlazorReservationsDbContext _context;

        public FoglalasRepository(IDbContextFactory<BlazorReservationsDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task CreateAsync(Foglalas foglalas)
        {
            _context.Foglalasok.Add(foglalas);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Foglalas>> ReadAllAsync()
        {
            return await _context.Foglalasok.Include(f => f.SzobaRef).Include(f => f.VendegRef).ToListAsync();
        }

        public async Task<Foglalas?> ReadByIdAsync(int id)
        {
            return await _context.Foglalasok
                .Include(f => f.SzobaRef)
                    .ThenInclude(sz => sz.KategoriaRef)
                .Include(f => f.VendegRef)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateAsync(Foglalas foglalas)
        {
            _context.Entry(foglalas).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(_context.Foglalasok.Single(f => f.Id == id));
            await _context.SaveChangesAsync();
        }
    }
}
