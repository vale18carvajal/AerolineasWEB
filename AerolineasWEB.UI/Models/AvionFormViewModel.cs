using AerolineasWEB.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AerolineasWEB.UI.Models
{
    public class AvionFormViewModel
    {
        // Datos del avión
        public int? id_avion { get; set; }
        public string? matricula { get; set; }
        public string? modelo { get; set; }
        public string? fabricante { get; set; }
        public int? cantidad_pasajeros { get; set; }
        public string? anio { get; set; }
        public EstadoAvion? estado { get; set; }
        public int? id_aerolinea { get; set; }
        public int? id_propietario { get; set; }

        //Listas para los selects
        //public SelectList Aerolineas { get; set; }
        //public SelectList Propietarios { get; set; }
        public List<Aerolinea> Aerolineas { get; set; }
        public List<Propietario> Propietarios { get; set; }
        
    }
}
