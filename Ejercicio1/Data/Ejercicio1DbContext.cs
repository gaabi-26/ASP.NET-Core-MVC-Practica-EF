using Ejercicio1.Models;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio1.Data
{
    public class Ejercicio1DbContext : DbContext
    {
        public Ejercicio1DbContext(DbContextOptions<Ejercicio1DbContext> options) : base(options)
        {
        }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Elemento> Elementos { get; set; }

        // Desactivar la eliminacion en cascada para las relaciones entre Pokemon y Elemento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.TipoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Debilidad)
                .WithMany()
                .HasForeignKey(p => p.DebilidadId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
    