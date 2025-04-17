
using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Xml.Linq;

namespace BlazorReservations.Repositories.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly BlazorReservationsDbContext _dbContext;

        public ValidationService(BlazorReservationsDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<List<string>> IsFoglalasValid(Foglalas foglalas)
        {
            var result = new List<string>();

            if (foglalas.Kezdete < DateTime.Today.Date)
            {
                result.Add("Új foglalás legkorábban a mai napra vehető fel.");
            }
            else if (foglalas.EjszakakSzama < 1)
            {
                result.Add("Szoba minimum egy éjszakára foglalható.");
            }
            else
            {
                var foglalasVege = GetFoglalasVege(foglalas.Kezdete, foglalas.EjszakakSzama);

                var existingFoglalasokForSzoba = await _dbContext.Foglalasok
                    .Where(f => f.SzobaId == foglalas.SzobaId && f.Id != foglalas.Id)
                    .ToListAsync();

                var overlapForSzobaExists = existingFoglalasokForSzoba.Any(f => (
                        (foglalas.Kezdete >= f.Kezdete && foglalas.Kezdete <= GetFoglalasVege(f.Kezdete, f.EjszakakSzama)) ||
                        (foglalasVege >= f.Kezdete && foglalasVege <= GetFoglalasVege(f.Kezdete, f.EjszakakSzama))
                    )
                );

                if (overlapForSzobaExists)
                {
                    result.Add($"A megadott időpontban ({foglalas.Kezdete} - {foglalasVege}) a szoba foglalt.");
                }
            }

            return result;
        }

        public async Task<List<string>> IsSzobaValid(Szoba szoba)
        {
            var result = new List<string>();

            if(szoba.SzobaSzam < 0)
            {
                result.Add("Érvénytelen szobaszám.");
            }
            else
            {
                var existingSzoba = await _dbContext.Szobak.AnyAsync(sz => sz.Id != szoba.Id && sz.SzobaSzam == szoba.SzobaSzam);

                if (existingSzoba)
                {
                    result.Add("Már létezik szoba ezzel a szobaszámmal.");
                }
            }

            if(szoba.AgyakSzama < 1)
            {
                result.Add("Egy szobában legalább egy ágy található.");
            }

            return result;
        }
        
        public async Task<List<string>> IsSzobaKategoriaValid(SzobaKategoria kategoria)
        {
            var result = new List<string>();
            var nameExists = await _dbContext.SzobaKategoriak
                .AnyAsync(k => k.Id != kategoria.Id && k.Megnevezes.ToLower().Equals(kategoria.Megnevezes.ToLower()));
 
            if (nameExists)
            {
                result.Add("Ez a kategória név már létezik.");
            }

            if (kategoria.EgysegAr < 1)
            {
                result.Add("Érvénytelen egységár.");
            }

            return result;
        }

        public List<string> IsVendegValid(Vendeg vendeg)
        {
            var result = new List<string>();
            if(!vendeg.Nev.Trim().Contains(' '))
            {
                result.Add("Teljes név megadása kötelező.");
            }

            return result;
        }
    }
}
