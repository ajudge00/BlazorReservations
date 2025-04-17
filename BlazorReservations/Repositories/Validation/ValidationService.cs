
using BlazorReservations.Data;
using BlazorReservations.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<string>> IsSzobaKategoriaValid(SzobaKategoria kategoria)
        {
            List<string> result = new List<string>();

            var nameExists = await _dbContext.SzobaKategoriak
                .AnyAsync(k => k.Megnevezes.ToLower().Equals(kategoria.Megnevezes.ToLower()));
 
            if (nameExists)
            {
                result.Add("Ez a kategória név már létezik.");
            }

            return result;
        }
    }
}
