
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AerolineasWEB.Model
{
    [Table("propietario")]
    public class Propietario
    {
        [Key]
        public int id_propietario { get; set; }
        [Required(ErrorMessage = "El campo identificación es obligatorio")]
        public string identificacion { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public TipoPropietario tipo { get; set; }
        [Required(ErrorMessage = "El campo pais es obligatorio")]
        public string pais { get; set; }
        [Required(ErrorMessage = "El campo telefono es obligatorio")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "El campo correo electrónico es obligatorio")]
        public string correo { get; set; }
        public EstadoPropietario estado { get; set; }
    }
}
