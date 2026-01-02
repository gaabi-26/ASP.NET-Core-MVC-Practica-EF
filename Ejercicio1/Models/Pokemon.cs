using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ejercicio1.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        [DisplayName("Número")]
        [Required(ErrorMessage = "El número es requerido.")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }
        [DisplayName("Imagen")]
        public string? UrlImagen { get; set; }
        [Required(ErrorMessage = "El tipo es requerido.")]
        public int TipoId { get; set; }
        [Required(ErrorMessage = "La debilidad es requerida.")]
        public int DebilidadId { get; set; }
        public Elemento? Tipo { get; set; }
        public Elemento? Debilidad { get; set; }
        public bool Activo { get; set; } = true;
    }
}
