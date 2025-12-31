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
    }
}
    