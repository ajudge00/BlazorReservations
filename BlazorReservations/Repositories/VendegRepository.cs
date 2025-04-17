﻿using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorReservations.Repositories
{
    public class VendegRepository : IVendegRepository
    {
        private readonly BlazorReservationsDbContext _context;

        public VendegRepository(IDbContextFactory<BlazorReservationsDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task CreateAsync(Vendeg vendeg)
        {
            _context.Vendegek.Add(vendeg);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vendeg>> ReadAllAsync()
        {
            return await _context.Vendegek.ToListAsync();
        }

        public async Task<Vendeg> ReadByIdAsync(int id)
        {
            return await _context.Vendegek.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateAsync(Vendeg vendeg)
        {
            _context.Entry(vendeg).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(_context.Vendegek.Single(v => v.Id == id));
            await _context.SaveChangesAsync();
        }
    }
}
