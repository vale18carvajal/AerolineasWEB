

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IAdministradorAvion
    {
        Task<IEnumerable<Avion>> ObtenerListaAvionesActivosAsync();
        Task<Avion> ObtenerAvionAsync(int id);
        Task<IEnumerable<Avion>> ObtenerPorNombreAerolineaAsync(string nombreAerolinea);
        Task<IEnumerable<Avion>> ObtenerPorNombrePropietarioAsync(string nombrePropietario);
        Task RegistrarAvionAsync(Avion avion);
        Task EditarAvionAsync(Avion avion);
        Task DesactivarAvionAsync(int id);
    }
}
