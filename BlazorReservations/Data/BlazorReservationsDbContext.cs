using BlazorReservations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BlazorReservations.Data
{
    public class BlazorReservationsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Foglalas> Foglalasok { get; set; }
        public DbSet<Szoba> Szobak {  get; set; }
        public DbSet<SzobaKategoria> SzobaKategoriak { get; set; }
        public DbSet<Vendeg> Vendegek {  get; set; }

        public BlazorReservationsDbContext(DbContextOptions<BlazorReservationsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Szoba>()
                .HasOne(s => s.KategoriaRef)
                .WithMany()
                .HasForeignKey(s => s.KategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Foglalas>()
                .HasOne(f => f.VendegRef)
                .WithMany()
                .HasForeignKey(f => f.VendegId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Foglalas>()
                .HasOne(f => f.SzobaRef)
                .WithMany()
                .HasForeignKey(f => f.SzobaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
