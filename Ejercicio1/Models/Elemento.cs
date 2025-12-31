using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Models
{
    public class Elemento
    {
        public int Id { get; set; }
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }
    }
}
