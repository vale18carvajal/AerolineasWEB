

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IAvionRepository
    {


            Task<IEnumerable<Avion>> obtenerAvionesActivosAsync();
            Task<Avion> obtenerPorIdAsync(int id);
            Task <IEnumerable<Avion>> obtenerPorNombreAerolineaAsync(string nombreAerolinea);
            Task <IEnumerable<Avion>> obtenerPorNombrePropietarioAsync(string nombrePropietario);
            Task crearAsync(Avion avion);
            Task editarAsync(Avion avion);
            Task desactivarAsync(int id);
 
    }
}
