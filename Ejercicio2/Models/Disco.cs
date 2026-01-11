using System.ComponentModel.DataAnnotations;

namespace Ejercicio2.Models
{
    public class Disco
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int CantidadCanciones { get; set; }
        public int TipoEdicionId { get; set; }
        public int EstiloId { get; set; }
        public Estilo? Estilo { get; set; }
        public TipoEdicion? TipoEdicion { get; set; }
    }
}
