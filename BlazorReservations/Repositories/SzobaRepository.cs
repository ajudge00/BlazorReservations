﻿using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorReservations.Repositories
{
    public class SzobaRepository : ISzobaRepository
    {
        private readonly BlazorReservationsDbContext _context;

        public SzobaRepository(IDbContextFactory<BlazorReservationsDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task CreateAsync(Szoba szoba)
        {
            _context.Szobak.Add(szoba);
            await _context.SaveChangesAsync();
        }
     
        public async Task<List<Szoba>> ReadAllAsync()
        {
            // az include azért kell, hogy listázásnál a kategória reference
            // értéke meg tudjon jelenni
            return await _context.Szobak.Include(s => s.KategoriaRef).ToListAsync();
        }

        public async Task<Szoba> ReadByIdAsync(int id)
        {
            return await _context.Szobak.FirstOrDefaultAsync(sz => sz.Id == id);
        }
        
        public async Task UpdateAsync(Szoba szoba)
        {
            _context.Entry(szoba).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            _context.Remove(_context.Szobak.Single(sz => sz.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task CreateKategoriaAsync(SzobaKategoria kategoria)
        {
            _context.SzobaKategoriak.Add(kategoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SzobaKategoria>> ReadKategoriaAllAsync()
        {
            return await _context.SzobaKategoriak.ToListAsync();
        }

        public async Task<SzobaKategoria> ReadKategoriaByIdAsync(int id)
        {
            return await _context.SzobaKategoriak.FirstOrDefaultAsync(szk => szk.Id == id);
        }

        public async Task UpdateKategoriaAsync(SzobaKategoria kategoria)
        {
            _context.Entry(kategoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
  
        public async Task DeleteKategoriaAsync(int id)
        {
            _context.Remove(_context.SzobaKategoriak.Single(szk => szk.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<SzobaKategoria>> ReadKategoriaFiltered(SzobaKategoriaFilter filter)
        {
            var query = _context.SzobaKategoriak.AsQueryable();

            if (filter is not null)
            {
                if (filter.Id is not null)
                {
                    query = query.Where(k => k.Id == filter.Id);
                }

                if (filter.EgysegAr is not null)
                {
                    query = query.Where(k => k.EgysegAr == filter.EgysegAr);
                }

            }

            return await query.ToListAsync();
        }

        public async Task<List<Szoba>> ReadFiltered(SzobaFilter filter)
        {
            var query = _context.Szobak.AsQueryable();

            if (filter is not null)
            {
                int szobaSzamInt = -1;
                if (filter.SzobaSzam is not null && int.TryParse(filter.SzobaSzam, out szobaSzamInt))
                {
                    query = query.Where(sz => sz.SzobaSzam == szobaSzamInt);
                }

                if (filter.AgyakSzama is not null)
                {
                    query = query.Where(sz => sz.AgyakSzama == filter.AgyakSzama);
                }

                if (filter.KategoriaId is not null)
                {
                    query = query.Where(sz => sz.KategoriaId == filter.KategoriaId);
                }

            }

            return await query.ToListAsync();
        }
    }
}
