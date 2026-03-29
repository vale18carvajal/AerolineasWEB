using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasWEB.Model
{
    [Table("aerolinea")]
    public class Aerolinea
    {
        [Key]
        public int id_aerolinea { set; get; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string nombre { set; get; }

        [Required(ErrorMessage = "El campo codigo IATA es obligatorio")]
        public string codigo_iata { set; get; }

        [Required(ErrorMessage = "El campo pais es obligatorio")]
        public string pais { set; get; }

        [Required(ErrorMessage = "El campo fecha fundación es obligatorio")]
        public DateTime fecha_fundacion { set; get; }
        public EstadoAerolinea estado { set; get;}
    }
}
