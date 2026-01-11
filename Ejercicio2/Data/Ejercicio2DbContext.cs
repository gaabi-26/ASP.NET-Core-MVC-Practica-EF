using Microsoft.EntityFrameworkCore;

namespace Ejercicio2.Data
{
    public class Ejercicio2DbContext : DbContext
    {
        public Ejercicio2DbContext(DbContextOptions<Ejercicio2DbContext> options) : base(options)
        {
        }

        public DbSet<Models.Disco> Discos { get; set; }
        public DbSet<Models.TipoEdicion> TiposEdicion { get; set; }
        public DbSet<Models.Estilo> Estilos { get; set; }
    }
}
