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

        private DateTime GetFoglalasVege(DateTime kezdete, int ejszakakSzama)
        {
            return new DateTime(
                kezdete.Year,
                kezdete.Month,
                kezdete.Day + ejszakakSzama,
                11,
                0,
                0
            );
        }

        public async Task<List<Foglalas>> ReadFiltered(FoglalasFilter filter)
        {
            var query = _context.Foglalasok.Include(f => f.VendegRef).AsQueryable();

            if (filter is not null)
            {
                if (filter.Kezdete is not null && filter.Vege is null)
                {
                    query = query.Where(f => f.Kezdete.Date == filter.Kezdete.Value.Date);
                }

                if (filter.Vege is not null && filter.Kezdete is null)
                {
                    query = query.Where(f => f.Kezdete.AddDays(f.EjszakakSzama).Date == filter.Vege.Value.Date);
                }

                if(filter.Kezdete is not null && filter.Vege is not null)
                {
                    query = query.Where(f => 
                                f.Kezdete <= filter.Vege &&
                                f.Kezdete >= filter.Kezdete &&
                                f.Kezdete.AddDays(f.EjszakakSzama).Date <= filter.Vege &&
                                f.Kezdete.AddDays(f.EjszakakSzama).Date >= filter.Kezdete
                                );
                }

                if (filter.EjszakakSzama is not null)
                {
                    query = query.Where(f => f.EjszakakSzama == filter.EjszakakSzama);
                }

                if(filter.VendegId is not null)
                {
                    query = query.Where(f => f.VendegId == filter.VendegId);
                }
                else if (filter.VendegNev is not null)
                {
                    query = query.Where(f => f.VendegRef.Nev == filter.VendegNev);
                }

                if(filter.SzobaId is not null)
                {
                    query = query.Where(f => f.SzobaId == filter.SzobaId);
                }

                if(filter.SzemelyekSzama is not null)
                {
                    query = query.Where(f => f.SzemelyekSzama == filter.SzemelyekSzama);
                }

            }

            return await query.ToListAsync();
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
