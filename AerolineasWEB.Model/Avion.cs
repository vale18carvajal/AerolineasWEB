

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasWEB.Model
{
    [Table("avion")]
    public class Avion
    {
        [Key]
        public int id_avion {  get; set; }

        [Required(ErrorMessage = "El campo modelo es obligatorio")]
        public string modelo { get; set; }

        [Required(ErrorMessage = "El campo fabricante es obligatorio")]
        public string fabricante { get; set;}

        [Required(ErrorMessage = "El campo cantidad de pasajaeros es obligatorio")]
        public int cantidad_pasajeros { get; set; }

        [Required(ErrorMessage = "El campo año es obligatorio")]
        public string anio { get; set; }
        public EstadoAvion estado { get; set; }

        [Required(ErrorMessage = "El campo aerolínea es obligatorio")]
        public int id_aerolinea { get; set; }

        [Required(ErrorMessage = "El campo propietario es obligatorio")]
        public int id_propietario { get; set; }
    }
}
