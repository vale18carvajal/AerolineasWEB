
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AerolineasWEB.Model
{
    [Table("propietario")]
    public class Propietario
    {
        [Key]
        public int id_propietario;
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string nombre;
        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public TipoPropietario tipo;
        [Required(ErrorMessage = "El campo pais es obligatorio")]
        public string pais;
        [Required(ErrorMessage = "El campo telefono es obligatorio")]
        public string telefono;
        [Required(ErrorMessage = "El campo correo electrónico es obligatorio")]
        public string correo;
        public EstadoPropietario estado;
    }
}
